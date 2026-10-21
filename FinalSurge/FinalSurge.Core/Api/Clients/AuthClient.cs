using FinalSurge.Core.Api.Models;
using RestSharp;

namespace FinalSurge.Core.Api.Clients;

public class AuthClient : BaseApiClient
{
    private const string LoginEndpoint = "login";

    public RestResponse<ApiResponse<LoginData>> Login(LoginRequest request) =>
        Post<ApiResponse<LoginData>>(LoginEndpoint, request);

    public RestResponse<ApiResponse<LoginData>> Login(string email, string password) =>
        Login(new LoginRequest { Email = email, Password = password });
}
