using Allure.Net.Commons;
using FinalSurge.Core.Configuration;
using FinalSurge.Core.Drivers;
using FinalSurge.Core.Pages;
using log4net;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace FinalSurge.Tests;

public class BaseTest
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BaseTest));

    protected IWebDriver driver = null!;

    protected static TestSettings Settings => ConfigReader.Settings;

    [SetUp]
    public void Setup()
    {
        Log.Info($"Test started: {TestContext.CurrentContext.Test.Name}");
        AllureApi.AddTestParameter("Browser", Settings.Browser.ToString());
        driver = DriverFactory.CreateDriver(Settings);
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

        try
        {
            CleanUp();
        }
        finally
        {
            driver.Quit();
            driver.Dispose();
        }
    }

    protected virtual void CleanUp()
    {
    }

    protected DashboardPage LoginAsDefaultUser() =>
        new LoginPage(driver).Load().Login(Settings.Email, Settings.Password);
}
