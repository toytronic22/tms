using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class CheckoutOverviewPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _cartItems = By.CssSelector("div[data-test='inventory-item']");
    private readonly By _lblItemTotal = By.CssSelector("div[data-test='subtotal-label']");
    private readonly By _lblTax = By.CssSelector("div[data-test='tax-label']");
    private readonly By _lblTotal = By.CssSelector("div[data-test='total-label']");
    private readonly By _btnFinish = By.Id("finish");
    private readonly By _btnCancel = By.Id("cancel");

    public CheckoutOverviewPage(IWebDriver driver) : base(driver)
    {
    }

    public string GetTitle() => _driver.FindElement(_lblTitle).Text;

    public int GetItemsCount() => _driver.FindElements(_cartItems).Count;

    public string GetItemTotalText() => _driver.FindElement(_lblItemTotal).Text;

    public string GetTaxText() => _driver.FindElement(_lblTax).Text;

    public string GetTotalText() => _driver.FindElement(_lblTotal).Text;

    public decimal GetItemTotal() => ParseMoney(GetItemTotalText());

    public decimal GetTax() => ParseMoney(GetTaxText());

    public decimal GetTotal() => ParseMoney(GetTotalText());

    public CheckoutCompletePage ClickFinish()
    {
        _driver.FindElement(_btnFinish).Click();
        return new CheckoutCompletePage(_driver);
    }

    public ProductsPage ClickCancel()
    {
        _driver.FindElement(_btnCancel).Click();
        return new ProductsPage(_driver);
    }

    public bool IsOverviewPageDisplayed() => IsDisplayed(_btnFinish) && IsDisplayed(_lblTotal);

    private static decimal ParseMoney(string text)
    {
        var value = text.Substring(text.IndexOf('$') + 1);
        return decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
    }
}
