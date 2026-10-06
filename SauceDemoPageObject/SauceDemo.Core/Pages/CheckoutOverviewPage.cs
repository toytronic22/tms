using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class CheckoutOverviewPage : BasePage<CheckoutOverviewPage>
{
    [FindsBy(How = How.CssSelector, Using = "span[data-test='title']")]
    private IWebElement _lblTitle = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='inventory-item-name']")]
    private IWebElement _lblItemName = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='total-label']")]
    private IWebElement _lblTotal = null!;

    [FindsBy(How = How.Id, Using = "finish")]
    private IWebElement _btnFinish = null!;

    private Label TitleLabel => new(_lblTitle);
    private Label ItemNameLabel => new(_lblItemName);
    private Label TotalLabel => new(_lblTotal);
    private Button FinishButton => new(_btnFinish);

    public CheckoutOverviewPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("/checkout-step-two.html") && FinishButton.IsDisplayed();

    public string GetTitle() => TitleLabel.GetText();

    public string GetFirstItemName() => ItemNameLabel.GetText();

    public string GetTotalText() => TotalLabel.GetText();

    public CheckoutCompletePage ClickFinish()
    {
        Log.Info("Finish order");
        FinishButton.Click();
        return new CheckoutCompletePage(_driver).Load();
    }
}
