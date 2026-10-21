using FinalSurge.Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace FinalSurge.Core.Drivers;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(TestSettings settings)
    {
        IWebDriver driver = settings.Browser switch
        {
            BrowserType.Chrome => CreateChromeDriver(settings),
            BrowserType.Firefox => CreateFirefoxDriver(settings),
            _ => throw new ArgumentException($"Unsupported browser: {settings.Browser}")
        };

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
        return driver;
    }

    private static IWebDriver CreateChromeDriver(TestSettings settings)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--guest");
        options.AddArgument("--window-size=1920,1080");

        if (settings.Headless)
        {
            options.AddArgument("--headless=new");
        }

        return string.IsNullOrEmpty(settings.GridUrl)
            ? new ChromeDriver(options)
            : new RemoteWebDriver(new Uri(settings.GridUrl), options);
    }

    private static IWebDriver CreateFirefoxDriver(TestSettings settings)
    {
        FirefoxOptions options = new FirefoxOptions();
        options.AddArgument("--width=1920");
        options.AddArgument("--height=1080");

        if (settings.Headless)
        {
            options.AddArgument("-headless");
        }

        return string.IsNullOrEmpty(settings.GridUrl)
            ? new FirefoxDriver(options)
            : new RemoteWebDriver(new Uri(settings.GridUrl), options);
    }
}
