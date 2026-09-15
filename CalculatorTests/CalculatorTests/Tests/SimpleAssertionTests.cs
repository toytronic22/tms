using CalculatorTests.Hooks;

namespace CalculatorTests.Tests;

[TestFixture]
public class SimpleAssertionTests : BaseTest
{
    [Test]
    public void AddReturnsSumOfTwoNumbers()
    {
        var result = Calculator.Add(5, 7);

        Assert.That(result, Is.EqualTo(12));
    }

    [Test]
    public void AddPositiveNumbersReturnsPositiveResult()
    {
        var result = Calculator.Add(12, 4);

        Assert.That(result, Is.Positive);
    }

    [Test]
    public void AddOppositeNumbersReturnsZero()
    {
        var result = Calculator.Add(88, -88);

        Assert.That(result, Is.Zero);
    }

    [Test]
    public void AddNegativeNumbersReturnsNegativeResult()
    {
        var result = Calculator.Add(-4, -6);

        Assert.That(result, Is.Negative);
    }

    [TestCase(12, -3, 9)]
    [TestCase(120, 0, 120)]
    [TestCase(-77, 4, -73)]
    [TestCase(-5, -5, -10)]
    public void AddDifferentNumbers(int firstNumber, int secondNumber, int expectedResult)
    {
        var result = Calculator.Add(firstNumber, secondNumber);

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
