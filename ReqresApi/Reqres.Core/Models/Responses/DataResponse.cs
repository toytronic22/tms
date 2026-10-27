using System.Text.Json.Serialization;

namespace Reqres.Core.Models.Responses;

public class DataResponse<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;
}
