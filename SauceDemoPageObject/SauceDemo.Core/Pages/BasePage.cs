using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Core.Pages;

public class BasePage
{
    protected static readonly ILog Log = LogManager.GetLogger(typeof(BasePage));

    protected IWebDriver _driver;

    public BasePage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void OpenSauceDemo()
    {
        Log.Info("Open SauceDemo");
        _driver.Navigate().GoToUrl("https://www.saucedemo.com");
        _driver.Manage().Window.Maximize();
    }

    public string GetUrl() => _driver.Url;

    protected WebDriverWait Wait(int seconds = 10) =>
        new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

    protected bool IsDisplayed(By locator)
    {
        var elements = _driver.FindElements(locator);
        return elements.Count > 0 && elements[0].Displayed;
    }
}
