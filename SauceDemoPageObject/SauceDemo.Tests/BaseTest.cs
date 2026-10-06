using Allure.Net.Commons;
using log4net;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SauceDemo.Core.Configuration;
using SauceDemo.Core.Drivers;
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

        driver = DriverFactory.CreateDriver(ConfigReader.Settings);
        new LoginPage(driver).Load();
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
