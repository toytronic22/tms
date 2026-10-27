using System.Net;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Api;
using FinalSurge.Core.Api.Clients;
using FinalSurge.Core.Api.Models;
using FinalSurge.Core.Models;
using FinalSurge.Core.Utils;
using RestSharp;

namespace FinalSurge.Tests.Api;

[AllureNUnit]
[AllureEpic("API")]
[AllureFeature("Workouts")]
[Category("API")]
public class WorkoutApiTests
{
    private WorkoutClient _client = null!;
    private readonly List<string> _createdWorkoutKeys = new();

    [SetUp]
    public void Setup()
    {
        _client = ApiSession.Workouts;
    }

    [TearDown]
    public void DeleteCreatedWorkouts()
    {
        foreach (string key in _createdWorkoutKeys)
        {
            _client.DeleteWorkout(key);
        }

        _createdWorkoutKeys.Clear();
    }

    [Test]
    [Category("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    public void CreateWorkout()
    {
        Workout workout = TestDataGenerator.RunWorkout();

        RestResponse<WorkoutSaveResponse> response = _client.SaveWorkout(WorkoutClient.ToRequest(workout));
        _createdWorkoutKeys.Add(response.Data!.NewWorkoutKey!);
        WorkoutItem? savedWorkout = _client.FindWorkout(response.Data.NewWorkoutKey!, workout.Date);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data.Success, Is.True);
            Assert.That(savedWorkout, Is.Not.Null);
            Assert.That(savedWorkout!.Name, Is.EqualTo(workout.Name));
            Assert.That(savedWorkout.Description, Is.EqualTo(workout.Description));
            Assert.That(savedWorkout.WorkoutDate, Is.EqualTo(workout.Date.ToString("yyyy-MM-dd'T'00:00:00")));
            Assert.That(savedWorkout.Activities[0].ActivityTypeName, Is.EqualTo("Run"));
            Assert.That(savedWorkout.Activities[0].Amount, Is.EqualTo(5));
            Assert.That(savedWorkout.Activities[0].AmountType, Is.EqualTo("km"));
            Assert.That(savedWorkout.Activities[0].Duration, Is.EqualTo(1800));
        });
    }

    [Test]
    [Category("Regression")]
    public void UpdateWorkout()
    {
        Workout workout = TestDataGenerator.RunWorkout();
        string key = _client.CreateWorkout(workout);
        _createdWorkoutKeys.Add(key);
        workout.Name = TestDataGenerator.UniqueName("Updated run");
        workout.Distance = 8;

        RestResponse<WorkoutSaveResponse> response = _client.SaveWorkout(WorkoutClient.ToRequest(workout, key));
        WorkoutItem? savedWorkout = _client.FindWorkout(key, workout.Date);

        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Success, Is.True);
            Assert.That(savedWorkout, Is.Not.Null);
            Assert.That(savedWorkout!.Name, Is.EqualTo(workout.Name));
            Assert.That(savedWorkout.Activities[0].Amount, Is.EqualTo(8));
        });
    }

    [Test]
    [Category("Regression")]
    public void DeleteWorkout()
    {
        Workout workout = TestDataGenerator.RunWorkout();
        string key = _client.CreateWorkout(workout);
        _createdWorkoutKeys.Add(key);

        RestResponse<ApiResponse> response = _client.DeleteWorkout(key);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data!.Success, Is.True);
            Assert.That(_client.FindWorkout(key, workout.Date), Is.Null);
        });
    }

    [Test]
    [Category("Regression")]
    public void DeleteNonExistentWorkoutReturnsError()
    {
        RestResponse<ApiResponse> response = _client.DeleteWorkout(Guid.NewGuid().ToString());

        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Success, Is.False);
            Assert.That(response.Data.ErrorNumber, Is.EqualTo(1));
            Assert.That(response.Data.ErrorDescription, Is.EqualTo("Workout not found"));
        });
    }

    [Test]
    [Category("Regression")]
    public void CreateWorkoutWithUnknownActivityTypeReturnsError()
    {
        WorkoutRequest request = WorkoutClient.ToRequest(TestDataGenerator.RunWorkout());
        request.Activity.ActivityTypeKey = Guid.NewGuid().ToString();

        RestResponse<WorkoutSaveResponse> response = _client.SaveWorkout(request);

        Assert.Multiple(() =>
        {
            Assert.That(response.Data!.Success, Is.False);
            Assert.That(response.Data.NewWorkoutKey, Is.Null);
            Assert.That(response.Data.ErrorDescription, Is.EqualTo("Permission Denied"));
        });
    }
}
