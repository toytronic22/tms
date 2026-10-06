using OpenQA.Selenium;
using SauceDemo.Core.Elements;

namespace SauceDemo.Core.Pages;

public class SideMenuSection : BasePage
{
    private readonly By _lnkLogout = By.Id("logout_sidebar_link");
    private readonly By _lnkResetAppState = By.Id("reset_sidebar_link");

    private Link LogoutLink => new(_driver.FindElement(_lnkLogout));
    private Link ResetAppStateLink => new(_driver.FindElement(_lnkResetAppState));

    public SideMenuSection(IWebDriver driver) : base(driver)
    {
    }

    public SideMenuSection WaitUntilOpened()
    {
        Wait().Until(_ => LogoutLink.IsDisplayed());
        return this;
    }

    public LoginPage ClickLogout()
    {
        LogoutLink.Click();
        return new LoginPage(_driver);
    }

    public SideMenuSection ResetAppState()
    {
        Log.Info("Reset app state");
        ResetAppStateLink.Click();
        return this;
    }
}
