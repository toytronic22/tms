using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class LoginPage : BasePage<LoginPage>
{
    [FindsBy(How = How.Id, Using = "user-name")]
    private IWebElement _inpUsername = null!;

    [FindsBy(How = How.Id, Using = "password")]
    private IWebElement _inpPassword = null!;

    [FindsBy(How = How.Id, Using = "login-button")]
    private IWebElement _btnLogin = null!;

    [FindsBy(How = How.CssSelector, Using = "h3[data-test='error']")]
    private IWebElement _msgError = null!;

    private Input UsernameInput => new(_inpUsername);
    private Input PasswordInput => new(_inpPassword);
    private Button LoginButton => new(_btnLogin);
    private Label ErrorMessage => new(_msgError);

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
        Log.Info("Open SauceDemo");
        _driver.Navigate().GoToUrl(BaseUrl);
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.StartsWith(BaseUrl) && LoginButton.IsDisplayed();

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
        return new ProductsPage(_driver).Load();
    }

    public LoginPage ClickLoginButtonExpectingError()
    {
        LoginButton.Click();
        return this;
    }

    public ProductsPage Login(string username = "standard_user", string password = "secret_sauce")
    {
        Log.Info($"Login as {username}");
        return SetUserName(username).SetPassword(password).ClickLoginButton();
    }

    public string GetErrorMessage() => ErrorMessage.GetText();

    public bool IsLoginPageDisplayed() =>
        LoginButton.IsDisplayed() && UsernameInput.IsDisplayed() && PasswordInput.IsDisplayed();
}
