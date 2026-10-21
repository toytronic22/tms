using FinalSurge.Core.Api.Models;
using FinalSurge.Core.Models;
using RestSharp;

namespace FinalSurge.Core.Api.Clients;

public class WorkoutClient : BaseApiClient
{
    private const string SaveEndpoint = "WorkoutSave";
    private const string ListEndpoint = "WorkoutList";
    private const string DeleteEndpoint = "WorkoutDelete";

    private static readonly Dictionary<string, string> ActivityTypeKeys = new()
    {
        ["Run"] = "00000001-0001-0001-0001-000000000001"
    };

    private readonly string _userKey;

    public WorkoutClient(string token, string userKey) : base(token)
    {
        _userKey = userKey;
    }

    public RestResponse<WorkoutSaveResponse> SaveWorkout(WorkoutRequest request) =>
        Post<WorkoutSaveResponse>(SaveEndpoint, request, new Dictionary<string, string>
        {
            ["scope"] = "USER",
            ["scope_key"] = _userKey
        });

    public RestResponse<ApiResponse<List<WorkoutItem>>> GetWorkouts(DateTime startDate, DateTime endDate) =>
        Get<ApiResponse<List<WorkoutItem>>>(ListEndpoint, new Dictionary<string, string>
        {
            ["scope"] = "USER",
            ["scopekey"] = _userKey,
            ["startdate"] = FormatDate(startDate),
            ["enddate"] = FormatDate(endDate),
            ["ishistory"] = "false",
            ["completedonly"] = "false"
        });

    public RestResponse<ApiResponse> DeleteWorkout(string workoutKey) =>
        Get<ApiResponse>(DeleteEndpoint, new Dictionary<string, string>
        {
            ["scope"] = "USER",
            ["scopekey"] = _userKey,
            ["workout_key"] = workoutKey
        });

    public static WorkoutRequest ToRequest(Workout workout, string? key = null) => new()
    {
        Key = key,
        WorkoutDate = FormatDateTime(workout.Date),
        Name = workout.Name,
        Description = workout.Description,
        Activity = new WorkoutActivity
        {
            ActivityTypeKey = ActivityTypeKeys[workout.ActivityType],
            ActivityTypeName = workout.ActivityType,
            Amount = workout.Distance,
            AmountType = workout.DistanceUnit,
            Duration = (int)workout.Duration.TotalSeconds
        }
    };

    public string CreateWorkout(Workout workout)
    {
        Log.Info($"Create workout {workout.Name} via API");
        WorkoutSaveResponse? response = SaveWorkout(ToRequest(workout)).Data;

        if (response?.Success != true || string.IsNullOrEmpty(response.NewWorkoutKey))
        {
            throw new InvalidOperationException($"Workout was not created: {response?.ErrorDescription}");
        }

        return response.NewWorkoutKey;
    }

    public WorkoutItem? FindWorkout(string workoutKey, DateTime date) =>
        GetWorkouts(date, date).Data?.Data?.FirstOrDefault(workout => workout.Key == workoutKey);
}
