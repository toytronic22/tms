using OpenQA.Selenium;

namespace SauceDemo.Core.Pages;

public class HeaderSection : BasePage
{
    private readonly By _btnBurgerMenu = By.Id("react-burger-menu-btn");
    private readonly By _btnLogout = By.Id("logout_sidebar_link");
    private readonly By _lnkCart = By.CssSelector("a[data-test='shopping-cart-link']");
    private readonly By _lblCartBadge = By.CssSelector("span[data-test='shopping-cart-badge']");

    public HeaderSection(IWebDriver driver) : base(driver)
    {
    }

    public HeaderSection OpenSideMenu()
    {
        _driver.FindElement(_btnBurgerMenu).Click();
        Wait().Until(e => e.FindElement(_btnLogout).Displayed);
        return this;
    }

    public LoginPage ClickLogoutButton()
    {
        _driver.FindElement(_btnLogout).Click();
        return new LoginPage(_driver);
    }

    public LoginPage Logout() => OpenSideMenu().ClickLogoutButton();

    public CartPage OpenCart()
    {
        _driver.FindElement(_lnkCart).Click();
        return new CartPage(_driver);
    }

    public bool IsCartIconDisplayed() => IsDisplayed(_lnkCart);

    public int GetCartCounter()
    {
        var badge = _driver.FindElements(_lblCartBadge);
        return badge.Count == 0 ? 0 : int.Parse(badge[0].Text);
    }
}
