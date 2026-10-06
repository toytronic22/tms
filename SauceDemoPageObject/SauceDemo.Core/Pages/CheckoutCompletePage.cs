using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class CheckoutCompletePage : BasePage<CheckoutCompletePage>
{
    [FindsBy(How = How.CssSelector, Using = "h2[data-test='complete-header']")]
    private IWebElement _lblCompleteHeader = null!;

    [FindsBy(How = How.Id, Using = "back-to-products")]
    private IWebElement _btnBackHome = null!;

    private Label CompleteHeaderLabel => new(_lblCompleteHeader);
    private Button BackHomeButton => new(_btnBackHome);

    public CheckoutCompletePage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("/checkout-complete.html") && CompleteHeaderLabel.IsDisplayed();

    public string GetCompleteHeader() => CompleteHeaderLabel.GetText();

    public ProductsPage ClickBackHome()
    {
        BackHomeButton.Click();
        return new ProductsPage(_driver).Load();
    }
}
