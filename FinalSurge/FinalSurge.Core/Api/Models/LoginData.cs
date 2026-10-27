using System.Text.Json.Serialization;

namespace FinalSurge.Core.Api.Models;

public class LoginData
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
