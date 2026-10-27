using System.Text.Json.Serialization;

namespace Reqres.Core.Models;

public class Resource
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("pantone_value")]
    public string PantoneValue { get; set; } = string.Empty;
}
