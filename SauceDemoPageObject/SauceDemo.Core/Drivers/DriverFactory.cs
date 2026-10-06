using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SauceDemo.Core.Configuration;

namespace SauceDemo.Core.Drivers;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(TestSettings settings)
    {
        IWebDriver driver = settings.Browser switch
        {
            BrowserType.Chrome => CreateChromeDriver(settings.Headless),
            BrowserType.Firefox => CreateFirefoxDriver(settings.Headless),
            _ => throw new ArgumentException($"Unsupported browser: {settings.Browser}")
        };

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
        return driver;
    }

    private static IWebDriver CreateChromeDriver(bool headless)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--guest");
        options.AddArgument("--start-maximized");

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool headless)
    {
        FirefoxOptions options = new FirefoxOptions();

        if (headless)
        {
            options.AddArgument("-headless");
        }

        IWebDriver driver = new FirefoxDriver(options);
        driver.Manage().Window.Maximize();
        return driver;
    }
}
