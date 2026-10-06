using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class ProductsPage : BasePage<ProductsPage>
{
    [FindsBy(How = How.CssSelector, Using = "span[data-test='title']")]
    private IWebElement _lblTitle = null!;

    [FindsBy(How = How.CssSelector, Using = "div[data-test='inventory-item']")]
    private IList<IWebElement> _itemsList = null!;

    [FindsBy(How = How.Id, Using = "add-to-cart-sauce-labs-backpack")]
    private IWebElement _btnAddBackpack = null!;

    [FindsBy(How = How.CssSelector, Using = "a[data-test='item-4-title-link']")]
    private IWebElement _lnkBackpackName = null!;

    private Label TitleLabel => new(_lblTitle);
    private Button AddBackpackButton => new(_btnAddBackpack);
    private Link BackpackNameLink => new(_lnkBackpackName);

    public HeaderSection Header => new(_driver);

    public ProductsPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad() => _driver.Navigate().GoToUrl($"{BaseUrl}/inventory.html");

    protected override bool EvaluateLoadedStatus() =>
        _driver.Url.Contains("/inventory.html") && TitleLabel.IsDisplayed();

    public string GetTitle() => TitleLabel.GetText();

    public int GetProductsCount() => _itemsList.Count;

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
        return new ProductDetailsPage(_driver).Load();
    }

    public bool IsProductsPageDisplayed() => TitleLabel.IsDisplayed() && GetProductsCount() > 0;
}
