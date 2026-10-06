using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class CheckoutCompletePage : BasePage
{
    private readonly By _lblCompleteHeader = By.CssSelector("h2[data-test='complete-header']");
    private readonly By _btnBackHome = By.Id("back-to-products");

    private Label CompleteHeaderLabel => new(_driver.FindElement(_lblCompleteHeader));
    private Button BackHomeButton => new(_driver.FindElement(_btnBackHome));

    public CheckoutCompletePage(IWebDriver driver) : base(driver)
    {
    }

    public string GetCompleteHeader()
    {
        Wait().Until(_ => CompleteHeaderLabel.IsDisplayed());
        return CompleteHeaderLabel.GetText();
    }

    public ProductsPage ClickBackHome()
    {
        BackHomeButton.Click();
        return new ProductsPage(_driver);
    }
}
