using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class CheckoutOverviewPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _lblTotal = By.CssSelector("div[data-test='total-label']");
    private readonly By _btnFinish = By.Id("finish");

    public CheckoutOverviewPage(IWebDriver driver) : base(driver)
    {
    }

    public string GetTitle() => _driver.FindElement(_lblTitle).Text;

    public string GetTotalText() => _driver.FindElement(_lblTotal).Text;

    public CheckoutCompletePage ClickFinish()
    {
        _driver.FindElement(_btnFinish).Click();
        return new CheckoutCompletePage(_driver);
    }
}
