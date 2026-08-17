using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TmsTests;

public abstract class BaseTest
{
    protected const string BaseUrl = "https://the-internet.herokuapp.com";

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
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
        Driver.Dispose();
    }

    protected void OpenPage(string path) => Driver.Navigate().GoToUrl($"{BaseUrl}{path}");
}
