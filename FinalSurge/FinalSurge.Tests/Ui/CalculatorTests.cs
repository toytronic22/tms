using Allure.NUnit;
using Allure.NUnit.Attributes;
using FinalSurge.Core.Pages;
using FinalSurge.Core.Pages.Calculators;

namespace FinalSurge.Tests.Ui;

[AllureNUnit]
[AllureEpic("UI")]
[AllureFeature("Calculators")]
[Category("UI")]
public class CalculatorTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    [TestCase("70", "kg", "180", "cm", "30", "male", "10", "km", "2,018 kCal", "700 kCal", "2,718 kCal")]
    [TestCase("150", "lbs", "65", "in", "25", "female", "5", "mi", "1,713 kCal", "547 kCal", "2,261 kCal")]
    public void CalculateCaloricNeeds(string weight, string weightUnit, string height, string heightUnit, string age,
        string gender, string distance, string distanceUnit, string nonActivityCalories, string trainingCalories,
        string totalCalories)
    {
        CaloricNeedsCalculatorPage calculatorPage = LoginAsDefaultUser().Header.OpenOtherCalculators()
            .SetWeight(weight, weightUnit)
            .SetHeight(height, heightUnit)
            .SetAge(age)
            .SetGender(gender)
            .SetRunDistance(distance, distanceUnit)
            .Calculate();

        Assert.Multiple(() =>
        {
            Assert.That(calculatorPage.GetNonActivityCalories(), Is.EqualTo(nonActivityCalories));
            Assert.That(calculatorPage.GetTrainingCalories(), Is.EqualTo(trainingCalories));
            Assert.That(calculatorPage.GetTotalCalories(), Is.EqualTo(totalCalories));
        });
    }

    [Test]
    [Category("Regression")]
    public void CalculatePaceForDistance()
    {
        PaceCalculatorPage calculatorPage = LoginAsDefaultUser().Header.OpenOtherCalculators()
            .OpenPaceCalculator()
            .SetDistance("10", "Kilometers")
            .SetTime(TimeSpan.FromMinutes(50))
            .Calculate();

        Assert.Multiple(() =>
        {
            Assert.That(calculatorPage.GetPace("Kilometer"), Is.EqualTo("5:00 min/km"));
            Assert.That(calculatorPage.GetSpeed("Kilometer"), Is.EqualTo("12.0 kph"));
            Assert.That(calculatorPage.GetPace("Mile"), Is.EqualTo("8:02 min/mi"));
            Assert.That(calculatorPage.GetSpeed("Mile"), Is.EqualTo("7.5 mph"));
        });
    }

    [Test]
    [Category("Regression")]
    public void CalculatePaceForMarathon()
    {
        PaceCalculatorPage calculatorPage = LoginAsDefaultUser().Header.OpenOtherCalculators()
            .OpenPaceCalculator()
            .SelectRaceDistance("Marathon")
            .SetTime(new TimeSpan(3, 30, 0))
            .Calculate();

        Assert.Multiple(() =>
        {
            Assert.That(calculatorPage.GetPace("Kilometer"), Is.EqualTo("4:58 min/km"));
            Assert.That(calculatorPage.GetSpeed("Kilometer"), Is.EqualTo("12.1 kph"));
            Assert.That(calculatorPage.GetPace("Mile"), Is.EqualTo("8:00 min/mi"));
        });
    }
}
