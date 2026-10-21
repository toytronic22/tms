using FinalSurge.Core.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public class WorkoutDetailsPage : BasePage<WorkoutDetailsPage>
{
    [FindsBy(How = How.CssSelector, Using = "#EditProfile .activityTypeName")]
    private IWebElement _lblActivityType = null!;

    [FindsBy(How = How.XPath, Using = "//span[@class='activityTypeName']/../following-sibling::div[1]")]
    private IWebElement _lblName = null!;

    [FindsBy(How = How.CssSelector, Using = "#EditProfile .formSep small.muted")]
    private IWebElement _lblDate = null!;

    [FindsBy(How = How.CssSelector, Using = "#EditProfile p.testme")]
    private IWebElement _lblDescription = null!;

    [FindsBy(How = How.CssSelector, Using = "#EditProfile .label-info")]
    private IWebElement _lblStatistics = null!;

    [FindsBy(How = How.CssSelector, Using = "#EditProfile .label-info + .label")]
    private IWebElement _lblPace = null!;

    [FindsBy(How = How.Id, Using = "WorkoutEditLink")]
    private IWebElement _lnkUpdateWorkout = null!;

    private Label ActivityTypeLabel => new(_lblActivityType);
    private Label NameLabel => new(_lblName);
    private Label DateLabel => new(_lblDate);
    private Label DescriptionLabel => new(_lblDescription);
    private Label StatisticsLabel => new(_lblStatistics);
    private Label PaceLabel => new(_lblPace);
    private Link UpdateWorkoutLink => new(_lnkUpdateWorkout);

    private readonly string? _workoutKey;

    public WorkoutDetailsPage(IWebDriver driver, string? workoutKey = null) : base(driver)
    {
        _workoutKey = workoutKey;
    }

    protected override void ExecuteLoad()
    {
        Log.Info($"Open workout {_workoutKey}");
        _driver.Navigate().GoToUrl($"{BaseUrl}/WorkoutDetails.cshtml?id={_workoutKey}");
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("WorkoutDetails.cshtml") &&
        (_workoutKey == null || _driver.Url.Contains(_workoutKey)) &&
        NameLabel.IsDisplayed();

    public string GetWorkoutKey() => new Uri(_driver.Url).Query.Split("id=")[1].Split('&')[0];

    public string GetActivityType() => ActivityTypeLabel.GetText();

    public string GetName() => NameLabel.GetText();

    public string GetDate() => DateLabel.GetText();

    public string GetDescription() => DescriptionLabel.GetText().Replace("Workout Description:", string.Empty).Trim();

    public string GetStatistics() => StatisticsLabel.GetText();

    public string GetPace() => PaceLabel.GetText();

    public AddWorkoutPage ClickUpdateWorkout()
    {
        UpdateWorkoutLink.Click();
        return new AddWorkoutPage(_driver).Load();
    }
}
