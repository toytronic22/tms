using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Core.Configuration;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public abstract class BasePage<T> : SlowLoadableComponent<T> where T : BasePage<T>
{
    protected static readonly ILog Log = LogManager.GetLogger(typeof(T));

    protected readonly IWebDriver _driver;

    protected BasePage(IWebDriver driver) : base(TimeSpan.FromSeconds(10))
    {
        _driver = driver;
        PageFactory.InitElements(driver, this);
    }

    protected string BaseUrl => ConfigReader.Settings.BaseUrl;

    public string GetUrl() => _driver.Url;
}
