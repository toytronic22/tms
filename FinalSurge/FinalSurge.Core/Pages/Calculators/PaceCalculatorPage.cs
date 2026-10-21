using FinalSurge.Core.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages.Calculators;

public class PaceCalculatorPage : BasePage<PaceCalculatorPage>
{
    [FindsBy(How = How.Id, Using = "RunDist")]
    private IWebElement _inpDistance = null!;

    [FindsBy(How = How.Id, Using = "DistType")]
    private IWebElement _ddlDistanceUnit = null!;

    [FindsBy(How = How.Id, Using = "RaceDist")]
    private IWebElement _ddlRaceDistance = null!;

    [FindsBy(How = How.Id, Using = "TimeHH")]
    private IWebElement _inpHours = null!;

    [FindsBy(How = How.Id, Using = "TimeMM")]
    private IWebElement _inpMinutes = null!;

    [FindsBy(How = How.Id, Using = "TimeSS")]
    private IWebElement _inpSeconds = null!;

    [FindsBy(How = How.Id, Using = "saveButtonSettings")]
    private IWebElement _btnCalculate = null!;

    private Input DistanceInput => new(_inpDistance);
    private DropDown DistanceUnitDropDown => new(_ddlDistanceUnit);
    private DropDown RaceDistanceDropDown => new(_ddlRaceDistance);
    private Input HoursInput => new(_inpHours);
    private Input MinutesInput => new(_inpMinutes);
    private Input SecondsInput => new(_inpSeconds);
    private Button CalculateButton => new(_btnCalculate);

    public PaceCalculatorPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() => CalculateButton.IsDisplayed() && HoursInput.IsDisplayed();

    public PaceCalculatorPage SetDistance(string distance, string unit)
    {
        DistanceInput.SetText(distance);
        DistanceUnitDropDown.SelectByText(unit);
        return this;
    }

    public PaceCalculatorPage SelectRaceDistance(string raceDistance)
    {
        RaceDistanceDropDown.SelectByText(raceDistance);
        return this;
    }

    public PaceCalculatorPage SetTime(TimeSpan time)
    {
        HoursInput.SetText(time.Hours.ToString());
        MinutesInput.SetText(time.Minutes.ToString());
        SecondsInput.SetText(time.Seconds.ToString());
        return this;
    }

    public PaceCalculatorPage Calculate()
    {
        Log.Info("Calculate paces");
        CalculateButton.Click();
        WaitForReload(_btnCalculate);
        return new PaceCalculatorPage(_driver).Load();
    }

    private string GetCell(string pacePer, int column) =>
        _driver.FindElement(By.XPath($"//table[1]//tr[td[1][normalize-space()='{pacePer}']]/td[{column}]")).Text.Trim();

    public string GetPace(string pacePer) => GetCell(pacePer, 2);

    public string GetSpeed(string pacePer) => GetCell(pacePer, 3);
}
