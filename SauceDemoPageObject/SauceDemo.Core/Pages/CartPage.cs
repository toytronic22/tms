using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class CartPage : BasePage<CartPage>
{
    [FindsBy(How = How.CssSelector, Using = "span[data-test='title']")]
    private IWebElement _lblTitle = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='inventory-item']")]
    private IList<IWebElement> _cartItems = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='inventory-item-name']")]
    private IWebElement _lblItemName = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='item-quantity']")]
    private IWebElement _lblQuantity = null!;

    [FindsBy(How = How.Id, Using = "checkout")]
    private IWebElement _btnCheckout = null!;

    [FindsBy(How = How.Id, Using = "remove-sauce-labs-backpack")]
    private IWebElement _btnRemoveBackpack = null!;

    private Label TitleLabel => new(_lblTitle);
    private Label ItemNameLabel => new(_lblItemName);
    private Label QuantityLabel => new(_lblQuantity);
    private Button CheckoutButton => new(_btnCheckout);
    private Button RemoveBackpackButton => new(_btnRemoveBackpack);

    public HeaderSection Header => new(_driver);

    public CartPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad() => _driver.Navigate().GoToUrl($"{BaseUrl}/cart.html");

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("/cart.html") && CheckoutButton.IsDisplayed();

    public string GetTitle() => TitleLabel.GetText();

    public int GetItemsCount() => _cartItems.Count;

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
        return new CheckoutInformationPage(_driver).Load();
    }

    public bool IsEmpty() => GetItemsCount() == 0;
}
