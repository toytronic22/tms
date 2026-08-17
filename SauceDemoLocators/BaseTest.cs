using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoLocators;

public abstract class BaseTest
{
    protected const string BaseUrl = "https://www.saucedemo.com";
    protected const string StandardUser = "standard_user";
    protected const string Password = "secret_sauce";

    protected IWebDriver Driver = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        Driver = new ChromeDriver(options);
        Driver.Manage().Window.Maximize();
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

        Driver.Navigate().GoToUrl(BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
        Driver.Dispose();
    }

    protected void LogIn()
    {
        Driver.FindElement(By.Id("user-name")).SendKeys(StandardUser);
        Driver.FindElement(By.Id("password")).SendKeys(Password);
        Driver.FindElement(By.Id("login-button")).Click();
    }
}
