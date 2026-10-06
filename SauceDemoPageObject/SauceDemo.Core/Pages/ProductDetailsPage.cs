using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class ProductDetailsPage : BasePage
{
    private readonly By _lblName = By.CssSelector("div[data-test='inventory-item-name']");
    private readonly By _lblPrice = By.CssSelector("div[data-test='inventory-item-price']");
    private readonly By _btnAddToCart = By.Id("add-to-cart");
    private readonly By _btnBackToProducts = By.Id("back-to-products");

    private Label NameLabel => new(_driver.FindElement(_lblName));
    private Label PriceLabel => new(_driver.FindElement(_lblPrice));
    private Button AddToCartButton => new(_driver.FindElement(_btnAddToCart));
    private Button BackToProductsButton => new(_driver.FindElement(_btnBackToProducts));

    public HeaderSection Header => new(_driver);

    public ProductDetailsPage(IWebDriver driver) : base(driver)
    {
    }

    public ProductDetailsPage WaitUntilOpened()
    {
        Wait().Until(_ => BackToProductsButton.IsDisplayed());
        return this;
    }

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
        return new ProductsPage(_driver);
    }
}
