using FinalSurge.Core.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FinalSurge.Core.Pages.Calculators;

public class CaloricNeedsCalculatorPage : BasePage<CaloricNeedsCalculatorPage>
{
    [FindsBy(How = How.Id, Using = "Weight")]
    private IWebElement _inpWeight = null!;

    [FindsBy(How = How.Id, Using = "HeightInchCent")]
    private IWebElement _inpHeight = null!;

    [FindsBy(How = How.Id, Using = "Age")]
    private IWebElement _inpAge = null!;

    [FindsBy(How = How.Id, Using = "RunDist")]
    private IWebElement _inpRunDistance = null!;

    [FindsBy(How = How.Id, Using = "saveButtonSettings")]
    private IWebElement _btnCalculate = null!;

    [FindsBy(How = How.LinkText, Using = "Pace Calculator")]
    private IWebElement _lnkPaceCalculator = null!;

    [FindsBy(How = How.CssSelector, Using = "table tbody td:nth-child(1)")]
    private IWebElement _lblNonActivityCalories = null!;

    [FindsBy(How = How.CssSelector, Using = "table tbody td:nth-child(2)")]
    private IWebElement _lblTrainingCalories = null!;

    [FindsBy(How = How.CssSelector, Using = "table tbody td:nth-child(3)")]
    private IWebElement _lblTotalCalories = null!;

    private Input WeightInput => new(_inpWeight);
    private Input HeightInput => new(_inpHeight);
    private Input AgeInput => new(_inpAge);
    private Input RunDistanceInput => new(_inpRunDistance);
    private Button CalculateButton => new(_btnCalculate);
    private Link PaceCalculatorLink => new(_lnkPaceCalculator);

    public CaloricNeedsCalculatorPage(IWebDriver driver) : base(driver)
    {
    }

    protected override void ExecuteLoad()
    {
    }

    protected override bool EvaluateLoadedStatus() => CalculateButton.IsDisplayed() && WeightInput.IsDisplayed();

    private RadioButton Option(string name, string value) =>
        new(_driver.FindElement(By.CssSelector($"input[name='{name}'][value='{value}']")));

    public CaloricNeedsCalculatorPage SetWeight(string weight, string unit)
    {
        WeightInput.SetText(weight);
        Option("WeightType", unit == "kg" ? "k" : "l").Select();
        return this;
    }

    public CaloricNeedsCalculatorPage SetHeight(string height, string unit)
    {
        HeightInput.SetText(height);
        Option("HeightType", unit == "cm" ? "m" : "i").Select();
        return this;
    }

    public CaloricNeedsCalculatorPage SetAge(string age)
    {
        AgeInput.SetText(age);
        return this;
    }

    public CaloricNeedsCalculatorPage SetGender(string gender)
    {
        Option("Gender", gender == "female" ? "f" : "m").Select();
        return this;
    }

    public CaloricNeedsCalculatorPage SetRunDistance(string distance, string unit)
    {
        RunDistanceInput.SetText(distance);
        Option("DistType", unit == "km" ? "k" : "m").Select();
        return this;
    }

    public CaloricNeedsCalculatorPage Calculate()
    {
        Log.Info("Calculate caloric needs");
        CalculateButton.Click();
        WaitForReload(_btnCalculate);
        return new CaloricNeedsCalculatorPage(_driver).Load();
    }

    public string GetNonActivityCalories() => new Label(_lblNonActivityCalories).GetText();

    public string GetTrainingCalories() => new Label(_lblTrainingCalories).GetText();

    public string GetTotalCalories() => new Label(_lblTotalCalories).GetText();

    public PaceCalculatorPage OpenPaceCalculator()
    {
        Log.Info("Open pace calculator");
        PaceCalculatorLink.Click();
        WaitForReload(_btnCalculate);
        return new PaceCalculatorPage(_driver).Load();
    }
}
