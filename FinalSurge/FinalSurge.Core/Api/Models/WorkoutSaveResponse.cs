using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class WorkoutSaveResponse : ApiResponse
{
    [JsonPropertyName("new_workout_key")]
    public string? NewWorkoutKey { get; set; }
}
