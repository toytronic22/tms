using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("deviceManufacturer")]
    public string DeviceManufacturer { get; set; } = string.Empty;

    [JsonPropertyName("deviceModel")]
    public string DeviceModel { get; set; } = string.Empty;

    [JsonPropertyName("deviceOperatingSystem")]
    public string DeviceOperatingSystem { get; set; } = string.Empty;

    [JsonPropertyName("deviceUniqueIdentifier")]
    public string DeviceUniqueIdentifier { get; set; } = string.Empty;
}
