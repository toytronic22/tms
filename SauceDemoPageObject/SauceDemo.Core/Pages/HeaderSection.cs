using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class HeaderSection : BasePage
{
    private readonly By _btnBurgerMenu = By.Id("react-burger-menu-btn");
    private readonly By _lnkCart = By.CssSelector("a[data-test='shopping-cart-link']");
    private readonly By _lblCartBadge = By.CssSelector("span[data-test='shopping-cart-badge']");

    private Button BurgerMenuButton => new(_driver.FindElement(_btnBurgerMenu));
    private Link CartLink => new(_driver.FindElement(_lnkCart));

    public HeaderSection(IWebDriver driver) : base(driver)
    {
    }

    public SideMenuSection OpenSideMenu()
    {
        BurgerMenuButton.Click();
        return new SideMenuSection(_driver).WaitUntilOpened();
    }

    public LoginPage Logout()
    {
        Log.Info("Logout");
        return OpenSideMenu().ClickLogout();
    }

    public CartPage OpenCart()
    {
        Log.Info("Open cart");
        CartLink.Click();
        return new CartPage(_driver).WaitUntilOpened();
    }

    public int GetCartCounter()
    {
        var badge = _driver.FindElements(_lblCartBadge);
        return badge.Count == 0 ? 0 : int.Parse(new Label(badge[0]).GetText());
    }
}
