using FinalSurge.Core.Api.Clients;
using FinalSurge.Core.Api.Models;
using FinalSurge.Core.Configuration;

namespace FinalSurge.Core.Api;

public static class ApiSession
{
    private static readonly Lazy<string> LazyToken = new(Login);
    private static readonly Lazy<string> LazyUserKey = new(LoadUserKey);

    public static string Token => LazyToken.Value;

    public static string UserKey => LazyUserKey.Value;

    public static WorkoutClient Workouts => new(Token, UserKey);

    public static CalendarLabelClient CalendarLabels => new(Token, UserKey);

    private static string Login()
    {
        TestSettings settings = ConfigReader.Settings;
        ApiResponse<LoginData>? response = new AuthClient().Login(settings.Email, settings.Password).Data;

        if (response?.Success != true || response.Data == null)
        {
            throw new InvalidOperationException($"API login failed: {response?.ErrorDescription}");
        }

        return response.Data.Token;
    }

    private static string LoadUserKey() => new SettingsClient(Token).GetSettings().Data!.Data!.UserKey;
}
