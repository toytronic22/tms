using Microsoft.Extensions.Configuration;

namespace SauceDemo.Core.Configuration;

public static class ConfigReader
{
    public static TestSettings Settings { get; } = ReadSettings();

    private static TestSettings ReadSettings()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        return configuration.GetSection("TestSettings").Get<TestSettings>()!;
    }
}
