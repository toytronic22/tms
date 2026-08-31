using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

public class LoginTests : BaseTest
{
    [Test]
    public void LoginWithValidUser()
    {
        LoginPage loginPage = new LoginPage(driver);
        ProductsPage productsPage = loginPage.Login();

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
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(username: "locked_out_user");

        Assert.That(loginPage.GetErrorMessage(),
            Is.EqualTo("Epic sadface: Sorry, this user has been locked out."));
    }

    [Test]
    public void LogoutReturnsToLoginPage()
    {
        LoginPage loginPage = new LoginPage(driver);
        ProductsPage productsPage = loginPage.Login();
        LoginPage loginPageAfterLogout = productsPage.Header.Logout();

        Assert.That(loginPageAfterLogout.IsLoginPageDisplayed(), Is.True);
    }
}
