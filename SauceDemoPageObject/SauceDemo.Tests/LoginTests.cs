using Allure.NUnit;
using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

[AllureNUnit]
public class LoginTests : BaseTest
{
    [Test]
    public void LoginWithValidUser()
    {
        ProductsPage productsPage = new LoginPage(driver).Login();

        Assert.Multiple(() =>
        {
            Assert.That(productsPage.IsProductsPageDisplayed(), Is.True);
            Assert.That(productsPage.GetTitle(), Is.EqualTo("Products"));
            Assert.That(productsPage.GetUrl(), Does.Contain("/inventory.html"));
        });
    }

    [Test]
    public void LoginWithLockedUser()
    {
        string errorMessage = new LoginPage(driver)
            .SetUserName("locked_out_user")
            .SetPassword("secret_sauce")
            .ClickLoginButtonExpectingError()
            .GetErrorMessage();

        Assert.That(errorMessage, Is.EqualTo("Epic sadface: Sorry, this user has been locked out."));
    }

    [Test]
    public void LogoutReturnsToLoginPage()
    {
        LoginPage loginPage = new LoginPage(driver)
            .Login()
            .Header.Logout();

        Assert.That(loginPage.IsLoginPageDisplayed(), Is.True);
    }
}
