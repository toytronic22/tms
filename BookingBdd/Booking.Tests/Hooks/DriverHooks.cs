using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;

namespace Booking.Tests.Hooks;

[Binding]
public class DriverHooks
{
    private readonly IObjectContainer _container;

    public DriverHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario]
    public void StartDriver()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--lang=en-GB");
        options.AddUserProfilePreference("intl.accept_languages", "en-GB");
        options.AddArgument("--start-maximized");

        IWebDriver driver = new ChromeDriver(options);
        _container.RegisterInstanceAs(driver);
    }

    [AfterScenario]
    public void QuitDriver()
    {
        IWebDriver driver = _container.Resolve<IWebDriver>();
        driver.Quit();
        driver.Dispose();
    }
}
