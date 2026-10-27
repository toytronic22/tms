using FinalSurge.Core.Api;
using FinalSurge.Core.Api.Clients;
using FinalSurge.Core.Configuration;
using FinalSurge.Core.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace FinalSurge.Bdd.StepDefinitions;

[Binding]
public class LoginSteps
{
    private readonly IWebDriver _driver;
    private LoginPage _loginPage = null!;
    private DashboardPage _dashboardPage = null!;

    private static TestSettings Settings => ConfigReader.Settings;

    public LoginSteps(IWebDriver driver)
    {
        _driver = driver;
    }

    [Given("I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        _loginPage = new LoginPage(_driver).Load();
    }

    [Given("I am logged in")]
    public void GivenIAmLoggedIn()
    {
        _dashboardPage = new LoginPage(_driver).Load().Login(Settings.Email, Settings.Password);
    }

    [When("I log in with valid credentials")]
    public void WhenILogInWithValidCredentials()
    {
        _dashboardPage = _loginPage.Login(Settings.Email, Settings.Password);
    }

    [When("I log in with email {string} and password {string}")]
    public void WhenILogInWithEmailAndPassword(string email, string password)
    {
        _loginPage.SetEmail(email).SetPassword(password).ClickLoginButtonExpectingError();
    }

    [When("I log out")]
    public void WhenILogOut()
    {
        _loginPage = _dashboardPage.Header.Logout();
    }

    [Then("the dashboard is displayed")]
    public void ThenTheDashboardIsDisplayed()
    {
        Assert.That(_dashboardPage.IsDashboardDisplayed(), Is.True);
    }

    [Then("my first name is shown in the header")]
    public void ThenMyFirstNameIsShownInTheHeader()
    {
        string firstName = new SettingsClient(ApiSession.Token).GetSettings().Data!.Data!.FirstName;

        Assert.That(_dashboardPage.Header.GetUserName(), Is.EqualTo(firstName));
    }

    [Then("the error message {string} is shown")]
    public void ThenTheErrorMessageIsShown(string message)
    {
        Assert.That(_loginPage.GetErrorMessage(), Is.EqualTo(message));
    }

    [Then("the email validation message {string} is shown")]
    public void ThenTheEmailValidationMessageIsShown(string message)
    {
        Assert.That(_loginPage.GetEmailValidationMessage(), Is.EqualTo(message));
    }

    [Then("the password validation message {string} is shown")]
    public void ThenThePasswordValidationMessageIsShown(string message)
    {
        Assert.That(_loginPage.GetPasswordValidationMessage(), Is.EqualTo(message));
    }

    [Then("the login page is displayed")]
    public void ThenTheLoginPageIsDisplayed()
    {
        Assert.That(_loginPage.IsLoginPageDisplayed(), Is.True);
    }
}
