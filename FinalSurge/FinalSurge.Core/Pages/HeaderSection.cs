using FinalSurge.Core.Elements;
using FinalSurge.Core.Pages.Calculators;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages;

public class HeaderSection : BasePage<HeaderSection>
{
    [FindsBy(How = How.CssSelector, Using = ".user-info strong")]
    private IWebElement _lblUserName = null!;

    [FindsBy(How = How.CssSelector, Using = ".user-info a[href='logout.cshtml']")]
    private IWebElement _lnkLogout = null!;

    [FindsBy(How = How.CssSelector, Using = "a[data-reveal-id='OtherCalc']")]
    private IWebElement _lnkOtherCalculators = null!;

    [FindsBy(How = How.Id, Using = "OtherCalciFrame")]
    private IWebElement _frmOtherCalculators = null!;

    private Label UserNameLabel => new(_lblUserName);
    private Link LogoutLink => new(_lnkLogout);
    private Link OtherCalculatorsLink => new(_lnkOtherCalculators);

    public HeaderSection(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() => LogoutLink.IsDisplayed();

    public string GetUserName() => UserNameLabel.GetText();

    public LoginPage Logout()
    {
        Log.Info("Logout");
        LogoutLink.Click();
        return new LoginPage(_driver).Load();
    }

    public CaloricNeedsCalculatorPage OpenOtherCalculators()
    {
        Log.Info("Open other calculators");
        OtherCalculatorsLink.Click();
        Waiter.Until(_ => _frmOtherCalculators.Displayed &&
                        (_frmOtherCalculators.GetAttribute("src") ?? string.Empty).Contains("CaloricNeedsCalc"));
        _driver.SwitchTo().Frame(_frmOtherCalculators);
        return new CaloricNeedsCalculatorPage(_driver).Load();
    }
}
