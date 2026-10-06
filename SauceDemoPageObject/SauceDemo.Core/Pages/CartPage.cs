using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class CartPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _cartItems = By.CssSelector("div[data-test='inventory-item']");
    private readonly By _lblItemName = By.CssSelector("div[data-test='inventory-item-name']");
    private readonly By _lblQuantity = By.CssSelector("div[data-test='item-quantity']");
    private readonly By _btnCheckout = By.Id("checkout");
    private readonly By _btnRemoveBackpack = By.Id("remove-sauce-labs-backpack");

    private Label TitleLabel => new(_driver.FindElement(_lblTitle));
    private Label ItemNameLabel => new(_driver.FindElement(_lblItemName));
    private Label QuantityLabel => new(_driver.FindElement(_lblQuantity));
    private Button CheckoutButton => new(_driver.FindElement(_btnCheckout));
    private Button RemoveBackpackButton => new(_driver.FindElement(_btnRemoveBackpack));

    public HeaderSection Header => new(_driver);

    public CartPage(IWebDriver driver) : base(driver)
    {
    }

    public CartPage WaitUntilOpened()
    {
        Wait().Until(_ => CheckoutButton.IsDisplayed());
        return this;
    }

    public string GetTitle() => TitleLabel.GetText();

    public int GetItemsCount() => _driver.FindElements(_cartItems).Count;

    public string GetFirstItemName() => ItemNameLabel.GetText();

    public string GetFirstItemQuantity() => QuantityLabel.GetText();

    public CartPage RemoveBackpack()
    {
        Log.Info("Remove backpack from cart");
        RemoveBackpackButton.Click();
        return this;
    }

    public CheckoutInformationPage ClickCheckout()
    {
        Log.Info("Start checkout");
        CheckoutButton.Click();
        return new CheckoutInformationPage(_driver);
    }

    public bool IsEmpty() => GetItemsCount() == 0;
}
