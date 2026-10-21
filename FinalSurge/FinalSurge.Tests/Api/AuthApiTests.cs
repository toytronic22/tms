using System.Net;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Api;
using FinalSurge.Core.Api.Clients;
using FinalSurge.Core.Api.Models;
using FinalSurge.Core.Configuration;
using RestSharp;

namespace FinalSurge.Tests.Api;

[AllureNUnit]
[AllureEpic("API")]
[AllureFeature("Authorization")]
[Category("API")]
public class AuthApiTests
{
    private static TestSettings Settings => ConfigReader.Settings;

    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.blocker)]
    public void LoginWithValidCredentialsReturnsToken()
    {
        RestResponse<ApiResponse<LoginData>> response = new AuthClient().Login(Settings.Email, Settings.Password);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data!.Success, Is.True);
            Assert.That(response.Data.Data!.Token, Is.Not.Empty);
        });
    }

    [Test]
    [Category("Regression")]
    [TestCase("nobody.tms@example.com", "wrong-password")]
    [TestCase("nobody.tms@example.com", "")]
    [TestCase("", "wrong-password")]
    public void LoginWithInvalidCredentialsReturnsError(string email, string password)
    {
        RestResponse<ApiResponse<LoginData>> response = new AuthClient().Login(email, password);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data!.Success, Is.False);
            Assert.That(response.Data.ErrorNumber, Is.EqualTo(1));
            Assert.That(response.Data.ErrorDescription, Is.EqualTo("Invalid login credentials. Please try again."));
            Assert.That(response.Data.Data, Is.Null);
        });
    }

    [Test]
    [Category("Smoke")]
    public void SettingsReturnCurrentUser()
    {
        RestResponse<ApiResponse<UserSettings>> response = new SettingsClient(ApiSession.Token).GetSettings();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data!.Success, Is.True);
            Assert.That(response.Data.Data!.Email, Is.EqualTo(Settings.Email).IgnoreCase);
            Assert.That(response.Data.Data.UserKey, Is.Not.Empty);
        });
    }

    [Test]
    [Category("Regression")]
    public void RequestWithoutTokenReturnsUnauthorized()
    {
        RestResponse<ApiResponse<UserSettings>> response = new SettingsClient(null).GetSettings();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}
