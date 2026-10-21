using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class UserSettings
{
    [JsonPropertyName("user_key")]
    public string UserKey { get; set; } = string.Empty;

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}
