using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class CheckoutOverviewPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _lblItemName = By.CssSelector("div[data-test='inventory-item-name']");
    private readonly By _lblTotal = By.CssSelector("div[data-test='total-label']");
    private readonly By _btnFinish = By.Id("finish");

    private Label TitleLabel => new(_driver.FindElement(_lblTitle));
    private Label ItemNameLabel => new(_driver.FindElement(_lblItemName));
    private Label TotalLabel => new(_driver.FindElement(_lblTotal));
    private Button FinishButton => new(_driver.FindElement(_btnFinish));

    public CheckoutOverviewPage(IWebDriver driver) : base(driver)
    {
    }

    public CheckoutOverviewPage WaitUntilOpened()
    {
        Wait().Until(_ => FinishButton.IsDisplayed());
        return this;
    }

    public string GetTitle() => TitleLabel.GetText();

    public string GetFirstItemName() => ItemNameLabel.GetText();

    public string GetTotalText() => TotalLabel.GetText();

    public CheckoutCompletePage ClickFinish()
    {
        Log.Info("Finish order");
        FinishButton.Click();
        return new CheckoutCompletePage(_driver);
    }
}
