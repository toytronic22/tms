using System.Globalization;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Api;
using FinalSurge.Core.Models;
using FinalSurge.Core.Pages;
using FinalSurge.Core.Utils;

namespace FinalSurge.Tests.Ui;

[AllureNUnit]
[AllureEpic("UI")]
[AllureFeature("Workouts")]
[Category("UI")]
public class WorkoutTests : BaseTest
{
    private readonly List<string> _createdWorkoutKeys = new();

    protected override void CleanUp()
    {
        foreach (string key in _createdWorkoutKeys)
        {
            ApiSession.Workouts.DeleteWorkout(key);
        }

        _createdWorkoutKeys.Clear();
    }

    private Workout CreateWorkoutViaApi()
    {
        Workout workout = TestDataGenerator.RunWorkout();
        _createdWorkoutKeys.Add(ApiSession.Workouts.CreateWorkout(workout));
        return workout;
    }

    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    public void AddRunWorkout()
    {
        Workout workout = TestDataGenerator.RunWorkout();
        LoginAsDefaultUser();

        WorkoutDetailsPage detailsPage = new AddWorkoutPage(driver).Load().AddWorkout(workout);
        _createdWorkoutKeys.Add(detailsPage.GetWorkoutKey());

        Assert.Multiple(() =>
        {
            Assert.That(detailsPage.GetActivityType(), Is.EqualTo("Run"));
            Assert.That(detailsPage.GetName(), Is.EqualTo(workout.Name));
            Assert.That(detailsPage.GetDescription(), Is.EqualTo(workout.Description));
            Assert.That(detailsPage.GetDate(),
                Is.EqualTo(workout.Date.ToString("dddd, MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US"))));
            Assert.That(detailsPage.GetStatistics(), Is.EqualTo("5.00 km ~ 30:00"));
            Assert.That(detailsPage.GetPace(), Is.EqualTo("9:39 min/mi"));
        });
    }

    [Test]
    [Category("Regression")]
    public void UpdateWorkoutName()
    {
        Workout workout = CreateWorkoutViaApi();
        string newName = TestDataGenerator.UniqueName("Updated run");
        LoginAsDefaultUser();

        WorkoutDetailsPage detailsPage = new WorkoutDetailsPage(driver, _createdWorkoutKeys[0]).Load()
            .ClickUpdateWorkout()
            .SetName(newName)
            .Save();

        Assert.Multiple(() =>
        {
            Assert.That(detailsPage.GetName(), Is.EqualTo(newName));
            Assert.That(detailsPage.GetDescription(), Is.EqualTo(workout.Description));
            Assert.That(ApiSession.Workouts.FindWorkout(_createdWorkoutKeys[0], workout.Date)?.Name, Is.EqualTo(newName));
        });
    }

    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    public void WorkoutCreatedViaApiIsShownInCalendar()
    {
        Workout workout = CreateWorkoutViaApi();
        LoginAsDefaultUser();

        bool isDisplayed = new CalendarPage(driver, workout.Date).Load().IsWorkoutDisplayed(workout.Name);

        Assert.That(isDisplayed, Is.True);
    }

    [Test]
    [Category("Regression")]
    public void DeleteWorkoutFromCalendar()
    {
        Workout workout = CreateWorkoutViaApi();
        LoginAsDefaultUser();

        CalendarPage calendarPage = new CalendarPage(driver, workout.Date).Load().DeleteWorkout(workout.Name);

        Assert.Multiple(() =>
        {
            Assert.That(calendarPage.IsWorkoutRemoved(workout.Name), Is.True);
            Assert.That(ApiSession.Workouts.FindWorkout(_createdWorkoutKeys[0], workout.Date), Is.Null);
        });
    }

    [Test]
    [Category("Regression")]
    public void CancelDeleteKeepsWorkoutInCalendar()
    {
        Workout workout = CreateWorkoutViaApi();
        LoginAsDefaultUser();
        CalendarPage calendarPage = new CalendarPage(driver, workout.Date).Load();

        ConfirmationModal modal = calendarPage.ClickDeleteWorkout(workout.Name);
        string message = modal.GetMessage();
        modal.Cancel();

        Assert.Multiple(() =>
        {
            Assert.That(message, Is.EqualTo("Are you sure you want to delete this workout from the calendar?"));
            Assert.That(calendarPage.IsWorkoutDisplayed(workout.Name), Is.True);
        });
    }
}
