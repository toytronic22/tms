using System.Globalization;
using FinalSurge.Bdd.Support;
using FinalSurge.Core.Api;
using FinalSurge.Core.Api.Models;
using FinalSurge.Core.Models;
using FinalSurge.Core.Pages;
using FinalSurge.Core.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace FinalSurge.Bdd.StepDefinitions;

[Binding]
public class WorkoutSteps
{
    private readonly IWebDriver _driver;
    private readonly WorkoutContext _context;
    private WorkoutDetailsPage _detailsPage = null!;
    private CalendarPage _calendarPage = null!;

    public WorkoutSteps(IWebDriver driver, WorkoutContext context)
    {
        _driver = driver;
        _context = context;
    }

    [Given("a run workout is created via API")]
    public void GivenARunWorkoutIsCreatedViaApi()
    {
        Workout workout = TestDataGenerator.RunWorkout();
        _context.Remember(workout, ApiSession.Workouts.CreateWorkout(workout));
    }

    [When("I add a workout with the following data:")]
    public void WhenIAddAWorkoutWithTheFollowingData(DataTable table)
    {
        Dictionary<string, string> data = table.Rows.ToDictionary(row => row["Field"], row => row["Value"]);
        Workout workout = new()
        {
            ActivityType = data["Activity"],
            Date = DateTime.Today,
            Name = TestDataGenerator.UniqueName(data["Name"]),
            Description = data["Description"],
            Distance = double.Parse(data["Distance"], CultureInfo.InvariantCulture),
            DistanceUnit = data["Unit"],
            Duration = TimeSpan.Parse(data["Duration"], CultureInfo.InvariantCulture)
        };

        _detailsPage = new AddWorkoutPage(_driver).Load().AddWorkout(workout);
        _context.Remember(workout, _detailsPage.GetWorkoutKey());
    }

    [When("I open the workout details")]
    public void WhenIOpenTheWorkoutDetails()
    {
        _detailsPage = new WorkoutDetailsPage(_driver, _context.WorkoutKey).Load();
    }

    [When("I change the workout name")]
    public void WhenIChangeTheWorkoutName()
    {
        _context.Workout.Name = TestDataGenerator.UniqueName("Updated run");
        _detailsPage = _detailsPage.ClickUpdateWorkout().SetName(_context.Workout.Name).Save();
    }

    [When("I open the calendar on the workout date")]
    public void WhenIOpenTheCalendarOnTheWorkoutDate()
    {
        _calendarPage = new CalendarPage(_driver, _context.Workout.Date).Load();
    }

    [When("I delete the workout from the calendar")]
    public void WhenIDeleteTheWorkoutFromTheCalendar()
    {
        _calendarPage.DeleteWorkout(_context.Workout.Name);
    }

    [Then("the workout details show the entered data")]
    public void ThenTheWorkoutDetailsShowTheEnteredData()
    {
        Workout workout = _context.Workout;

        Assert.Multiple(() =>
        {
            Assert.That(_detailsPage.GetActivityType(), Is.EqualTo(workout.ActivityType));
            Assert.That(_detailsPage.GetName(), Is.EqualTo(workout.Name));
            Assert.That(_detailsPage.GetDescription(), Is.EqualTo(workout.Description));
            Assert.That(_detailsPage.GetDate(),
                Is.EqualTo(workout.Date.ToString("dddd, MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US"))));
        });
    }

    [Then("the workout statistics are {string}")]
    public void ThenTheWorkoutStatisticsAre(string statistics)
    {
        Assert.That(_detailsPage.GetStatistics(), Is.EqualTo(statistics));
    }

    [Then("the workout pace is {string}")]
    public void ThenTheWorkoutPaceIs(string pace)
    {
        Assert.That(_detailsPage.GetPace(), Is.EqualTo(pace));
    }

    [Then("the workout is saved in the training log")]
    public void ThenTheWorkoutIsSavedInTheTrainingLog()
    {
        WorkoutItem? savedWorkout = ApiSession.Workouts.FindWorkout(_context.WorkoutKey, _context.Workout.Date);

        Assert.That(savedWorkout?.Name, Is.EqualTo(_context.Workout.Name));
    }

    [Then("the workout is displayed in the calendar")]
    public void ThenTheWorkoutIsDisplayedInTheCalendar()
    {
        Assert.That(_calendarPage.IsWorkoutDisplayed(_context.Workout.Name), Is.True);
    }

    [Then("the workout is not displayed in the calendar")]
    public void ThenTheWorkoutIsNotDisplayedInTheCalendar()
    {
        Assert.That(_calendarPage.IsWorkoutRemoved(_context.Workout.Name), Is.True);
    }

    [Then("the workout is removed from the training log")]
    public void ThenTheWorkoutIsRemovedFromTheTrainingLog()
    {
        Assert.That(ApiSession.Workouts.FindWorkout(_context.WorkoutKey, _context.Workout.Date), Is.Null);
    }
}
