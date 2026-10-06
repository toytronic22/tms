using OpenQA.Selenium;
using SauceDemo.Core.Elements;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Core.Pages;

public class SideMenuSection : BasePage<SideMenuSection>
{
    [FindsBy(How = How.Id, Using = "react-burger-menu-btn")]
    private IWebElement _btnBurgerMenu = null!;

    [FindsBy(How = How.Id, Using = "logout_sidebar_link")]
    private IWebElement _lnkLogout = null!;

    [FindsBy(How = How.Id, Using = "reset_sidebar_link")]
    private IWebElement _lnkResetAppState = null!;

    private Button BurgerMenuButton => new(_btnBurgerMenu);
    private Link LogoutLink => new(_lnkLogout);
    private Link ResetAppStateLink => new(_lnkResetAppState);

    public SideMenuSection(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad() => BurgerMenuButton.Click();

    protected override bool EvaluateLoadedStatus() => LogoutLink.IsDisplayed();

    public LoginPage ClickLogout()
    {
        LogoutLink.Click();
        return new LoginPage(_driver).Load();
    }

    public SideMenuSection ResetAppState()
    {
        Log.Info("Reset app state");
        ResetAppStateLink.Click();
        return this;
    }
}
