using OpenQA.Selenium;

namespace FinalSurge.Core.Pages;

public class DashboardPage : BasePage<DashboardPage>
{
    public HeaderSection Header => new HeaderSection(_driver).Load();

    public DashboardPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
        Log.Info("Open dashboard");
        _driver.Navigate().GoToUrl($"{BaseUrl}/Default.cshtml");
    }

    protected override bool EvaluateLoadedStatus() => _driver.Title == "Final Surge - Dashboard";

    public bool IsDashboardDisplayed() => EvaluateLoadedStatus() && Header.GetUserName().Length > 0;
}
