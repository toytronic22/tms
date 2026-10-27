using Reqres.Core.Models.Requests;
using Reqres.Core.Models.Responses;
using RestSharp;

namespace Reqres.Core.Clients;

public class AuthClient : ApiClient
{
    private const string RegisterEndpoint = "api/register";
    private const string LoginEndpoint = "api/login";

    public AuthClient(string baseUrl) : base(baseUrl)
    {
    }

    public RestResponse<T> Register<T>(AuthRequest request)
    {
        return Post<T>(RegisterEndpoint, request);
    }

    public RestResponse<T> Login<T>(AuthRequest request)
    {
        return Post<T>(LoginEndpoint, request);
    }
}
