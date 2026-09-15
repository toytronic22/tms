using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

public class BaseTest
{
    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--guest");
        options.AddArgument("--start-maximized");

        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        new BasePage(driver).OpenSauceDemo();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}
