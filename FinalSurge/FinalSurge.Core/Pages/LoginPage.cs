using FinalSurge.Core.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public class LoginPage : BasePage<LoginPage>
{
    [FindsBy(How = How.Id, Using = "login_name")]
    private IWebElement _inpEmail = null!;

    [FindsBy(How = How.Id, Using = "login_password")]
    private IWebElement _inpPassword = null!;

    [FindsBy(How = How.CssSelector, Using = "#login-validate button[type='submit']")]
    private IWebElement _btnLogin = null!;

    [FindsBy(How = How.CssSelector, Using = ".alert.alert-error")]
    private IWebElement _msgError = null!;

    [FindsBy(How = How.CssSelector, Using = "label.error[for='login_name']")]
    private IWebElement _msgEmailValidation = null!;

    [FindsBy(How = How.CssSelector, Using = "label.error[for='login_password']")]
    private IWebElement _msgPasswordValidation = null!;

    private Input EmailInput => new(_inpEmail);
    private Input PasswordInput => new(_inpPassword);
    private Button LoginButton => new(_btnLogin);
    private Label ErrorMessage => new(_msgError);
    private Label EmailValidationMessage => new(_msgEmailValidation);
    private Label PasswordValidationMessage => new(_msgPasswordValidation);

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
        Log.Info("Open login page");
        _driver.Navigate().GoToUrl($"{BaseUrl}/login.cshtml");
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("login.cshtml", StringComparison.OrdinalIgnoreCase) && LoginButton.IsDisplayed();

    public LoginPage SetEmail(string email)
    {
        EmailInput.SetText(email);
        return this;
    }

    public LoginPage SetPassword(string password)
    {
        PasswordInput.SetText(password);
        return this;
    }

    public DashboardPage ClickLoginButton()
    {
        LoginButton.Click();
        new HeaderSection(_driver).Load();
        return new DashboardPage(_driver).Load();
    }

    public LoginPage ClickLoginButtonExpectingError()
    {
        LoginButton.Click();
        return this;
    }

    public DashboardPage Login(string email, string password)
    {
        Log.Info($"Login as {email}");
        return SetEmail(email).SetPassword(password).ClickLoginButton();
    }

    public string GetErrorMessage() => ErrorMessage.GetText();

    public string GetEmailValidationMessage() => EmailValidationMessage.GetText();

    public string GetPasswordValidationMessage() => PasswordValidationMessage.GetText();

    public bool IsLoginPageDisplayed() =>
        LoginButton.IsDisplayed() && EmailInput.IsDisplayed() && PasswordInput.IsDisplayed();
}
