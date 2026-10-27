using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Api;
using FinalSurge.Core.Api.Clients;
using FinalSurge.Core.Pages;

namespace FinalSurge.Tests.Ui;

[AllureNUnit]
[AllureEpic("UI")]
[AllureFeature("Login")]
[Category("UI")]
public class LoginTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.blocker)]
    public void LoginWithValidCredentials()
    {
        string firstName = new SettingsClient(ApiSession.Token).GetSettings().Data!.Data!.FirstName;

        DashboardPage dashboardPage = LoginAsDefaultUser();

        Assert.Multiple(() =>
        {
            Assert.That(dashboardPage.IsDashboardDisplayed(), Is.True);
            Assert.That(dashboardPage.Header.GetUserName(), Is.EqualTo(firstName));
        });
    }

    [Test]
    [Category("Regression")]
    public void LoginWithWrongCredentialsShowsError()
    {
        string errorMessage = new LoginPage(driver).Load()
            .SetEmail("nobody.tms@example.com")
            .SetPassword("wrong-password")
            .ClickLoginButtonExpectingError()
            .GetErrorMessage();

        Assert.That(errorMessage, Is.EqualTo("Invalid login credentials. Please try again."));
    }

    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    public void LogoutReturnsToLoginPage()
    {
        LoginPage loginPage = LoginAsDefaultUser().Header.Logout();

        Assert.That(loginPage.IsLoginPageDisplayed(), Is.True);
    }
}
