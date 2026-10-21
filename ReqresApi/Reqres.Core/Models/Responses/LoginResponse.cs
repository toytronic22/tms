using System.Text.Json.Serialization;

namespace Reqres.Core.Models.Responses;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
