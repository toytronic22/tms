using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class LoginPage : BasePage
{
    private readonly By _inpUsername = By.Id("user-name");
    private readonly By _inpPassword = By.Id("password");
    private readonly By _btnLogin = By.Id("login-button");
    private readonly By _msgError = By.CssSelector("h3[data-test='error']");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public LoginPage SetUserName(string username)
    {
        _driver.FindElement(_inpUsername).Clear();
        _driver.FindElement(_inpUsername).SendKeys(username);
        return this;
    }

    public LoginPage SetPassword(string password)
    {
        _driver.FindElement(_inpPassword).Clear();
        _driver.FindElement(_inpPassword).SendKeys(password);
        return this;
    }

    public ProductsPage ClickLoginButton()
    {
        _driver.FindElement(_btnLogin).Click();
        return new ProductsPage(_driver);
    }

    public ProductsPage Login(string username = "standard_user", string password = "secret_sauce") =>
        SetUserName(username).SetPassword(password).ClickLoginButton();

    public string GetErrorMessage() => _driver.FindElement(_msgError).Text;

    public bool IsLoginPageDisplayed() =>
        IsDisplayed(_btnLogin) && IsDisplayed(_inpUsername) && IsDisplayed(_inpPassword);
}
