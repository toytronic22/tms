using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class ProductDetailsPage : BasePage<ProductDetailsPage>
{
    [FindsBy(How = How.CssSelector, Using = "div[data-test='inventory-item-name']")]
    private IWebElement _lblName = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='inventory-item-price']")]
    private IWebElement _lblPrice = null!;

    [FindsBy(How = How.Id, Using = "add-to-cart")]
    private IWebElement _btnAddToCart = null!;

    [FindsBy(How = How.Id, Using = "back-to-products")]
    private IWebElement _btnBackToProducts = null!;

    private Label NameLabel => new(_lblName);
    private Label PriceLabel => new(_lblPrice);
    private Button AddToCartButton => new(_btnAddToCart);
    private Button BackToProductsButton => new(_btnBackToProducts);

    public HeaderSection Header => new(_driver);

    public ProductDetailsPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("/inventory-item.html") && BackToProductsButton.IsDisplayed();

    public string GetName() => NameLabel.GetText();

    public string GetPrice() => PriceLabel.GetText();

    public ProductDetailsPage AddToCart()
    {
        Log.Info($"Add {GetName()} to cart");
        AddToCartButton.Click();
        return this;
    }

    public ProductsPage ClickBackToProducts()
    {
        BackToProductsButton.Click();
        return new ProductsPage(_driver).Load();
    }
}
