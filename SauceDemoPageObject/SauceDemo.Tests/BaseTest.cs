using Allure.Net.Commons;
using log4net;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Core.Pages;

namespace SauceDemo.Tests;

public class BaseTest
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BaseTest));

    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        Log.Info($"Test started: {TestContext.CurrentContext.Test.Name}");

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
        TestContext.ResultAdapter result = TestContext.CurrentContext.Result;

        if (result.Outcome.Status == TestStatus.Failed)
        {
            Log.Error($"Test failed: {TestContext.CurrentContext.Test.Name} - {result.Message}");
            AllureApi.AddAttachment(
                "screenshot",
                "image/png",
                ((ITakesScreenshot)driver).GetScreenshot().AsByteArray,
                ".png");
        }
        else
        {
            Log.Info($"Test passed: {TestContext.CurrentContext.Test.Name}");
        }

        driver.Quit();
        driver.Dispose();
    }
}
