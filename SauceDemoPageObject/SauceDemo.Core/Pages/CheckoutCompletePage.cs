using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class CheckoutCompletePage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _lblCompleteHeader = By.CssSelector("h2[data-test='complete-header']");
    private readonly By _lblCompleteText = By.CssSelector("div[data-test='complete-text']");
    private readonly By _btnBackHome = By.Id("back-to-products");

    public HeaderSection Header => new(_driver);

    public CheckoutCompletePage(IWebDriver driver) : base(driver)
    {
    }

    public string GetTitle() => _driver.FindElement(_lblTitle).Text;

    public string GetCompleteHeader() => _driver.FindElement(_lblCompleteHeader).Text;

    public string GetCompleteText() => _driver.FindElement(_lblCompleteText).Text;

    public ProductsPage ClickBackHome()
    {
        _driver.FindElement(_btnBackHome).Click();
        return new ProductsPage(_driver);
    }

    public bool IsOrderCompleted() => IsDisplayed(_lblCompleteHeader);
}
