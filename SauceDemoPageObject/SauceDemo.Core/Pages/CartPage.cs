using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class CartPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _cartItems = By.CssSelector("div[data-test='inventory-item']");
    private readonly By _lblItemName = By.CssSelector("div[data-test='inventory-item-name']");
    private readonly By _lblQuantity = By.CssSelector("div[data-test='item-quantity']");
    private readonly By _btnContinueShopping = By.Id("continue-shopping");
    private readonly By _btnCheckout = By.Id("checkout");
    private readonly By _btnRemoveBackpack = By.Id("remove-sauce-labs-backpack");

    public HeaderSection Header => new(_driver);

    public CartPage(IWebDriver driver) : base(driver)
    {
    }

    public string GetTitle() => _driver.FindElement(_lblTitle).Text;

    public int GetItemsCount() => _driver.FindElements(_cartItems).Count;

    public string GetFirstItemName() => _driver.FindElement(_lblItemName).Text;

    public string GetFirstItemQuantity() => _driver.FindElement(_lblQuantity).Text;

    public CartPage RemoveBackpack()
    {
        _driver.FindElement(_btnRemoveBackpack).Click();
        return this;
    }

    public ProductsPage ClickContinueShopping()
    {
        _driver.FindElement(_btnContinueShopping).Click();
        return new ProductsPage(_driver);
    }

    public CheckoutInformationPage ClickCheckout()
    {
        _driver.FindElement(_btnCheckout).Click();
        return new CheckoutInformationPage(_driver);
    }

    public bool IsCartPageDisplayed() => IsDisplayed(_btnCheckout) && IsDisplayed(_lblTitle);

    public bool IsEmpty() => GetItemsCount() == 0;
}
