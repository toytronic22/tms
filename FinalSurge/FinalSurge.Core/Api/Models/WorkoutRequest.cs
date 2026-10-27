using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class WorkoutRequest
{
    [JsonPropertyName("key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Key { get; set; }

    [JsonPropertyName("workout_date")]
    public string WorkoutDate { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("is_race")]
    public bool IsRace { get; set; }

    [JsonPropertyName("Activity")]
    public WorkoutActivity Activity { get; set; } = new();
}
