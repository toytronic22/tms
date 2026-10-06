using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class HeaderSection : BasePage<HeaderSection>
{
    [FindsBy(How = How.CssSelector, Using = "a[data-test='shopping-cart-link']")]
    private IWebElement _lnkCart = null!;

    [FindsBy(How = How.CssSelector, Using = "span[data-test='shopping-cart-badge']")]
    private IList<IWebElement> _lblCartBadge = null!;

    private Link CartLink => new(_lnkCart);

    public HeaderSection(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() => CartLink.IsDisplayed();

    public SideMenuSection OpenSideMenu() => new SideMenuSection(_driver).Load();

    public LoginPage Logout()
    {
        Log.Info("Logout");
        return OpenSideMenu().ClickLogout();
    }

    public CartPage OpenCart()
    {
        Log.Info("Open cart");
        CartLink.Click();
        return new CartPage(_driver).Load();
    }

    public int GetCartCounter() =>
        _lblCartBadge.Count == 0 ? 0 : int.Parse(new Label(_lblCartBadge[0]).GetText());
}
