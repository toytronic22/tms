using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class ProductsPage : BasePage
{
    private readonly By _lblTitle = By.CssSelector("span[data-test='title']");
    private readonly By _itemsList = By.CssSelector("div[data-test='inventory-item']");
    private readonly By _btnAddBackpack = By.Id("add-to-cart-sauce-labs-backpack");

    public HeaderSection Header => new(_driver);

    public ProductsPage(IWebDriver driver) : base(driver)
    {
    }

    public string GetTitle() => _driver.FindElement(_lblTitle).Text;

    public int GetProductsCount() => _driver.FindElements(_itemsList).Count;

    public ProductsPage AddBackpackToCart()
    {
        _driver.FindElement(_btnAddBackpack).Click();
        return this;
    }

    public bool IsProductsPageDisplayed() => IsDisplayed(_lblTitle) && GetProductsCount() > 0;
}
