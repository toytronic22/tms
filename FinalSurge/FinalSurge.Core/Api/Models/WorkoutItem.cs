using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class WorkoutItem
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("workout_date")]
    public string WorkoutDate { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("workout_status_text")]
    public string? StatusText { get; set; }

    [JsonPropertyName("Activities")]
    public List<WorkoutActivity> Activities { get; set; } = new();
}
