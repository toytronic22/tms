using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class LoginPage : BasePage
{
    private readonly By _inpUsername = By.Id("user-name");
    private readonly By _inpPassword = By.Id("password");
    private readonly By _btnLogin = By.Id("login-button");
    private readonly By _msgError = By.CssSelector("h3[data-test='error']");

    private Input UsernameInput => new(_driver.FindElement(_inpUsername));
    private Input PasswordInput => new(_driver.FindElement(_inpPassword));
    private Button LoginButton => new(_driver.FindElement(_btnLogin));
    private Label ErrorMessage => new(_driver.FindElement(_msgError));

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public LoginPage SetUserName(string username)
    {
        UsernameInput.SetText(username);
        return this;
    }

    public LoginPage SetPassword(string password)
    {
        PasswordInput.SetText(password);
        return this;
    }

    public ProductsPage ClickLoginButton()
    {
        LoginButton.Click();
        return new ProductsPage(_driver);
    }

    public ProductsPage Login(string username = "standard_user", string password = "secret_sauce")
    {
        Log.Info($"Login as {username}");
        return SetUserName(username).SetPassword(password).ClickLoginButton();
    }

    public string GetErrorMessage() => ErrorMessage.GetText();

    public bool IsLoginPageDisplayed() =>
        IsDisplayed(_btnLogin) && IsDisplayed(_inpUsername) && IsDisplayed(_inpPassword);
}
