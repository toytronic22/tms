using System.Globalization;
using FinalSurge.Core.Elements;
using FinalSurge.Core.Models;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public class AddWorkoutPage : BasePage<AddWorkoutPage>
{
    [FindsBy(How = How.Id, Using = "WorkoutDate")]
    private IWebElement _inpDate = null!;

    [FindsBy(How = How.Id, Using = "Name")]
    private IWebElement _inpName = null!;

    [FindsBy(How = How.Id, Using = "Desc")]
    private IWebElement _inpDescription = null!;

    [FindsBy(How = How.Id, Using = "Distance")]
    private IWebElement _inpDistance = null!;

    [FindsBy(How = How.Id, Using = "DistType")]
    private IWebElement _ddlDistanceUnit = null!;

    [FindsBy(How = How.Id, Using = "Duration")]
    private IWebElement _inpDuration = null!;

    [FindsBy(How = How.Id, Using = "saveButton")]
    private IWebElement _btnSave = null!;

    private Input DateInput => new(_inpDate);
    private Input NameInput => new(_inpName);
    private Input DescriptionInput => new(_inpDescription);
    private Input DistanceInput => new(_inpDistance);
    private DropDown DistanceUnitDropDown => new(_ddlDistanceUnit);
    private Input DurationInput => new(_inpDuration);
    private Button SaveButton => new(_btnSave);

    public AddWorkoutPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
        Log.Info("Open add workout page");
        _driver.Navigate().GoToUrl($"{BaseUrl}/WorkoutAdd.cshtml");
    }

    protected override bool EvaluateLoadedStatus() => _driver.Title == "Final Surge - Add Workout";

    public AddWorkoutPage SelectActivityType(string activityType)
    {
        Log.Info($"Select activity type {activityType}");
        IWebElement typeLink = _driver.FindElement(
            By.XPath($"//a[contains(@class,'typeselector') and normalize-space()='{activityType}']"));
        typeLink.Click();

        IWebElement noSubTypeLink = _driver.FindElement(
            By.CssSelector($"{typeLink.GetAttribute("data-target")} li.subtypeselector a"));
        Waiter.Until(_ => noSubTypeLink.Displayed);
        noSubTypeLink.Click();

        Waiter.Until(_ => NameInput.IsDisplayed());
        return this;
    }

    public AddWorkoutPage SetDate(DateTime date)
    {
        DateInput.SetText(date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
        _inpDate.SendKeys(Keys.Escape);
        return this;
    }

    public AddWorkoutPage SetName(string name)
    {
        NameInput.SetText(name);
        return this;
    }

    public AddWorkoutPage SetDescription(string description)
    {
        DescriptionInput.SetText(description);
        return this;
    }

    public AddWorkoutPage SetDistance(double distance, string unit)
    {
        DistanceInput.SetText(distance.ToString(CultureInfo.InvariantCulture));
        DistanceUnitDropDown.SelectByValue(unit);
        return this;
    }

    public AddWorkoutPage SetDuration(TimeSpan duration)
    {
        DurationInput.SetText(duration.ToString(@"hh\:mm\:ss"));
        return this;
    }

    public AddWorkoutPage FillWorkout(Workout workout)
    {
        Log.Info($"Fill workout {workout.Name}");
        return SetDate(workout.Date)
            .SetName(workout.Name)
            .SetDescription(workout.Description)
            .SetDistance(workout.Distance, workout.DistanceUnit)
            .SetDuration(workout.Duration);
    }

    public WorkoutDetailsPage Save()
    {
        Log.Info("Save workout");
        WaitForReload(_btnSave, SaveButton.Click);
        return new WorkoutDetailsPage(_driver).Load();
    }

    public WorkoutDetailsPage AddWorkout(Workout workout) =>
        SelectActivityType(workout.ActivityType).FillWorkout(workout).Save();

    public string GetName() => NameInput.GetValue();
}
