using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class WorkoutActivity
{
    [JsonPropertyName("activity_type_key")]
    public string ActivityTypeKey { get; set; } = string.Empty;

    [JsonPropertyName("activity_type_name")]
    public string ActivityTypeName { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public double? Amount { get; set; }

    [JsonPropertyName("amount_type")]
    public string? AmountType { get; set; }

    [JsonPropertyName("duration")]
    public double? Duration { get; set; }
}
