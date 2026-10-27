using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class CalendarLabel
{
    [JsonPropertyName("key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Key { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("back_color")]
    public string BackColor { get; set; } = string.Empty;

    [JsonPropertyName("fore_color")]
    public string ForeColor { get; set; } = string.Empty;

    [JsonPropertyName("start_date")]
    public string StartDate { get; set; } = string.Empty;

    [JsonPropertyName("end_date")]
    public string EndDate { get; set; } = string.Empty;
}
