using FinalSurge.Core.Configuration;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public abstract class BasePage<T> : SlowLoadableComponent<T> where T : BasePage<T>
{
    protected static readonly ILog Log = LogManager.GetLogger(typeof(T));

    protected readonly IWebDriver _driver;

    protected BasePage(IWebDriver driver) : base(TimeSpan.FromSeconds(15))
    {
        _driver = driver;
        PageFactory.InitElements(driver, this);
    }

    protected string BaseUrl => ConfigReader.Settings.BaseUrl;

    protected WebDriverWait Waiter => new(_driver, TimeSpan.FromSeconds(15));

    public string GetUrl() => _driver.Url;

    protected bool IsElementPresent(By by)
    {
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        try
        {
            return _driver.FindElements(by).Any(element => element.Displayed);
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
        finally
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConfigReader.Settings.ImplicitWaitSeconds);
        }
    }

    protected void WaitForReload(IWebElement element, Action action)
    {
        IWebElement oldElement = ((IWrapsElement)element).WrappedElement;
        action();
        Waiter.Until(driver =>
        {
            try
            {
                _ = oldElement.Enabled;
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        });
    }
}
