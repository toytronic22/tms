using Allure.Net.Commons;
using FinalSurge.Core.Api;
using FinalSurge.Core.Configuration;
using FinalSurge.Core.Drivers;
using log4net;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;

namespace FinalSurge.Bdd.Support;

[Binding]
public class Hooks
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(Hooks));

    private readonly IObjectContainer _container;
    private readonly ScenarioContext _scenarioContext;
    private readonly WorkoutContext _workoutContext;

    public Hooks(IObjectContainer container, ScenarioContext scenarioContext, WorkoutContext workoutContext)
    {
        _container = container;
        _scenarioContext = scenarioContext;
        _workoutContext = workoutContext;
    }

    [BeforeScenario]
    public void StartBrowser()
    {
        TestSettings settings = ConfigReader.Settings;
        Log.Info($"Scenario started: {_scenarioContext.ScenarioInfo.Title}");
        AllureApi.AddTestParameter("Browser", settings.Browser.ToString());
        _container.RegisterInstanceAs(DriverFactory.CreateDriver(settings));
    }

    [AfterScenario]
    public void CloseBrowser()
    {
        IWebDriver driver = _container.Resolve<IWebDriver>();

        if (_scenarioContext.TestError != null)
        {
            Log.Error($"Scenario failed: {_scenarioContext.ScenarioInfo.Title} - {_scenarioContext.TestError.Message}");
            AllureApi.AddAttachment(
                "screenshot",
                "image/png",
                ((ITakesScreenshot)driver).GetScreenshot().AsByteArray,
                ".png");
        }
        else
        {
            Log.Info($"Scenario passed: {_scenarioContext.ScenarioInfo.Title}");
        }

        try
        {
            foreach (string key in _workoutContext.CreatedWorkoutKeys)
            {
                ApiSession.Workouts.DeleteWorkout(key);
            }
        }
        finally
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
