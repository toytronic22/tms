using RestSharp;

namespace Reqres.Core.Clients;

public class ApiClient
{
    protected readonly RestClient Client;

    public ApiClient(string baseUrl)
    {
        Client = new RestClient(baseUrl);
    }

    public RestResponse<T> Get<T>(string endpoint, Dictionary<string, string>? queryParams = null)
    {
        RestRequest request = new RestRequest(endpoint);

        if (queryParams != null)
        {
            foreach (KeyValuePair<string, string> parameter in queryParams)
            {
                request.AddQueryParameter(parameter.Key, parameter.Value);
            }
        }

        return Client.Execute<T>(request);
    }

    public RestResponse<T> Post<T>(string endpoint, object body)
    {
        return Client.Execute<T>(CreateRequestWithBody(endpoint, Method.Post, body));
    }

    public RestResponse<T> Put<T>(string endpoint, object body)
    {
        return Client.Execute<T>(CreateRequestWithBody(endpoint, Method.Put, body));
    }

    public RestResponse<T> Patch<T>(string endpoint, object body)
    {
        return Client.Execute<T>(CreateRequestWithBody(endpoint, Method.Patch, body));
    }

    public RestResponse Delete(string endpoint)
    {
        return Client.Execute(new RestRequest(endpoint, Method.Delete));
    }

    private static RestRequest CreateRequestWithBody(string endpoint, Method method, object body)
    {
        RestRequest request = new RestRequest(endpoint, method);
        request.AddJsonBody(body);
        return request;
    }
}
