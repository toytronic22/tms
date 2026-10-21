using Microsoft.Extensions.Configuration;

namespace FinalSurge.Core.Configuration;

public static class ConfigReader
{
    public static TestSettings Settings { get; } = ReadSettings();

    private static TestSettings ReadSettings()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(typeof(ConfigReader).Assembly, optional: true)
            .AddEnvironmentVariables()
            .Build();

        return configuration.GetSection("TestSettings").Get<TestSettings>()!;
    }
}
