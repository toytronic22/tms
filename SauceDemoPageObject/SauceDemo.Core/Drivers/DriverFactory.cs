using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SauceDemo.Core.Configuration;

namespace SauceDemo.Core.Drivers;

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
        options.AddArgument("--start-maximized");

        if (settings.Headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }

        return string.IsNullOrEmpty(settings.GridUrl)
            ? new ChromeDriver(options)
            : new RemoteWebDriver(new Uri(settings.GridUrl), options);
    }

    private static IWebDriver CreateFirefoxDriver(TestSettings settings)
    {
        FirefoxOptions options = new FirefoxOptions();

        if (settings.Headless)
        {
            options.AddArgument("-headless");
        }

        IWebDriver driver = string.IsNullOrEmpty(settings.GridUrl)
            ? new FirefoxDriver(options)
            : new RemoteWebDriver(new Uri(settings.GridUrl), options);
        driver.Manage().Window.Maximize();
        return driver;
    }
}
