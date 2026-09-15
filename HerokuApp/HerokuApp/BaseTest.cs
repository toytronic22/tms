using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HerokuApp;

public class BaseTest
{
    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver(GetChromeOptions());
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    protected virtual ChromeOptions GetChromeOptions()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--guest");
        options.AddArgument("--start-maximized");
        return options;
    }
}
