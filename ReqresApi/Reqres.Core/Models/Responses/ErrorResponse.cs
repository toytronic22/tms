using System.Text.Json.Serialization;

namespace Reqres.Core.Models.Responses;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
}
