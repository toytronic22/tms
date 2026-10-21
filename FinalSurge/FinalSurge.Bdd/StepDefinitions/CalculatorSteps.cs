using System.Globalization;
using FinalSurge.Core.Pages;
using FinalSurge.Core.Pages.Calculators;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace FinalSurge.Bdd.StepDefinitions;

[Binding]
public class CalculatorSteps
{
    private readonly IWebDriver _driver;
    private CaloricNeedsCalculatorPage _caloricNeedsPage = null!;
    private PaceCalculatorPage _pacePage = null!;

    public CalculatorSteps(IWebDriver driver)
    {
        _driver = driver;
    }

    [Given("I open the other calculators")]
    public void GivenIOpenTheOtherCalculators()
    {
        _caloricNeedsPage = new HeaderSection(_driver).Load().OpenOtherCalculators();
    }

    [When("I enter weight {string} in {string}")]
    public void WhenIEnterWeight(string weight, string unit)
    {
        _caloricNeedsPage.SetWeight(weight, unit);
    }

    [When("I enter height {string} in {string}")]
    public void WhenIEnterHeight(string height, string unit)
    {
        _caloricNeedsPage.SetHeight(height, unit);
    }

    [When("I enter age {string} and gender {string}")]
    public void WhenIEnterAgeAndGender(string age, string gender)
    {
        _caloricNeedsPage.SetAge(age).SetGender(gender);
    }

    [When("I enter run distance {string} in {string}")]
    public void WhenIEnterRunDistance(string distance, string unit)
    {
        _caloricNeedsPage.SetRunDistance(distance, unit);
    }

    [When("I calculate caloric needs")]
    public void WhenICalculateCaloricNeeds()
    {
        _caloricNeedsPage.Calculate();
    }

    [When("I open the pace calculator")]
    public void WhenIOpenThePaceCalculator()
    {
        _pacePage = _caloricNeedsPage.OpenPaceCalculator();
    }

    [When("I calculate pace for {string} {string} with time {string}")]
    public void WhenICalculatePaceForDistance(string distance, string unit, string time)
    {
        _pacePage.SetDistance(distance, unit).SetTime(TimeSpan.Parse(time, CultureInfo.InvariantCulture)).Calculate();
    }

    [When("I calculate pace for race {string} with time {string}")]
    public void WhenICalculatePaceForRace(string race, string time)
    {
        _pacePage.SelectRaceDistance(race).SetTime(TimeSpan.Parse(time, CultureInfo.InvariantCulture)).Calculate();
    }

    [Then("the non-activity calories are {string}")]
    public void ThenTheNonActivityCaloriesAre(string calories)
    {
        Assert.That(_caloricNeedsPage.GetNonActivityCalories(), Is.EqualTo(calories));
    }

    [Then("the training calories are {string}")]
    public void ThenTheTrainingCaloriesAre(string calories)
    {
        Assert.That(_caloricNeedsPage.GetTrainingCalories(), Is.EqualTo(calories));
    }

    [Then("the total calories are {string}")]
    public void ThenTheTotalCaloriesAre(string calories)
    {
        Assert.That(_caloricNeedsPage.GetTotalCalories(), Is.EqualTo(calories));
    }

    [Then("the pace per kilometer is {string}")]
    public void ThenThePacePerKilometerIs(string pace)
    {
        Assert.That(_pacePage.GetPace("Kilometer"), Is.EqualTo(pace));
    }

    [Then("the pace per mile is {string}")]
    public void ThenThePacePerMileIs(string pace)
    {
        Assert.That(_pacePage.GetPace("Mile"), Is.EqualTo(pace));
    }
}
