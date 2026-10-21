using FinalSurge.Core.Api.Models;
using RestSharp;

namespace FinalSurge.Core.Api.Clients;

public class SettingsClient : BaseApiClient
{
    private const string SettingsEndpoint = "Settings";

    public SettingsClient(string? token) : base(token)
    {
    }

    public RestResponse<ApiResponse<UserSettings>> GetSettings() => Get<ApiResponse<UserSettings>>(SettingsEndpoint);
}
