using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class CheckoutCompletePage : BasePage
{
    private readonly By _lblCompleteHeader = By.CssSelector("h2[data-test='complete-header']");

    public CheckoutCompletePage(IWebDriver driver) : base(driver)
    {
    }

    public string GetCompleteHeader()
    {
        Wait().Until(d => d.FindElement(_lblCompleteHeader).Displayed);
        return _driver.FindElement(_lblCompleteHeader).Text;
    }
}
