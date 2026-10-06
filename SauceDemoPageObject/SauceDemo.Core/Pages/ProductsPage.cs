using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class ProductsPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _itemsList = By.CssSelector("div[data-test='inventory-item']");
    private readonly By _btnAddBackpack = By.Id("add-to-cart-sauce-labs-backpack");
    private readonly By _lnkBackpackName = By.CssSelector("a[data-test='item-4-title-link']");

    private Label TitleLabel => new(_driver.FindElement(_lblTitle));
    private Button AddBackpackButton => new(_driver.FindElement(_btnAddBackpack));
    private Link BackpackNameLink => new(_driver.FindElement(_lnkBackpackName));

    public HeaderSection Header => new(_driver);

    public ProductsPage(IWebDriver driver) : base(driver)
    {
    }

    public string GetTitle() => TitleLabel.GetText();

    public int GetProductsCount() => _driver.FindElements(_itemsList).Count;

    public ProductsPage AddBackpackToCart()
    {
        Log.Info("Add backpack to cart");
        AddBackpackButton.Click();
        return this;
    }

    public ProductDetailsPage OpenBackpackDetails()
    {
        Log.Info("Open backpack details");
        BackpackNameLink.Click();
        return new ProductDetailsPage(_driver).WaitUntilOpened();
    }

    public bool IsProductsPageDisplayed() => IsDisplayed(_lblTitle) && GetProductsCount() > 0;
}
