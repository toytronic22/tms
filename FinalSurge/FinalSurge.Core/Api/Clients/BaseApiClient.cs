using FinalSurge.Core.Configuration;
using log4net;
using RestSharp;

namespace FinalSurge.Core.Api.Clients;

public abstract class BaseApiClient
{
    protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseApiClient));

    private readonly RestClient _client;

    protected BaseApiClient(string? token = null)
    {
        _client = new RestClient(ConfigReader.Settings.ApiUrl);

        if (!string.IsNullOrEmpty(token))
        {
            _client.AddDefaultHeader("Authorization", $"Bearer {token}");
        }
    }

    protected RestResponse<T> Get<T>(string resource, Dictionary<string, string>? queryParams = null)
    {
        RestRequest request = new RestRequest(resource);
        AddQueryParams(request, queryParams);
        return Execute<T>(request);
    }

    protected RestResponse<T> Post<T>(string resource, object body, Dictionary<string, string>? queryParams = null)
    {
        RestRequest request = new RestRequest(resource, Method.Post);
        AddQueryParams(request, queryParams);
        request.AddJsonBody(body);
        return Execute<T>(request);
    }

    private static void AddQueryParams(RestRequest request, Dictionary<string, string>? queryParams)
    {
        if (queryParams == null)
        {
            return;
        }

        foreach (KeyValuePair<string, string> param in queryParams)
        {
            request.AddQueryParameter(param.Key, param.Value);
        }
    }

    private RestResponse<T> Execute<T>(RestRequest request)
    {
        Log.Info($"{request.Method} {request.Resource}");
        RestResponse<T> response = _client.Execute<T>(request);
        Log.Info($"Response {(int)response.StatusCode}: {response.Content}");
        return response;
    }

    protected static string FormatDate(DateTime date) => date.ToString("yyyy-MM-dd");

    protected static string FormatDateTime(DateTime date) => date.ToString("yyyy-MM-dd'T'00:00:00");
}
