using CalculatorTests.Hooks;
using CalculatorTests.TestData;

namespace CalculatorTests.Tests;

[TestFixture]
public class ComplexAssertionsTests : BaseTest
{
    [Test]
    public void GetMultiplicationResultsReturnsExpectedList()
    {
        var result = Calculator.GetMultiplicationResults(5);

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result, Is.EqualTo(new List<int> { 5, 10, 15, 20 }));
            Assert.That(result, Has.Member(20));
            Assert.That(result, Is.Unique);
        });
    }

    [Test]
    public void GetMultiplicationResultsAreOrderedAscending()
    {
        var result = Calculator.GetMultiplicationResults(3);

        Assert.That(result, Is.Ordered.Ascending);
    }

    [Test]
    public void GetResultAsStringDescribesAddition()
    {
        var result = Calculator.GetResultAsString(5, 8);

        Assert.Multiple(() =>
        {
            Assert.That(result, Does.StartWith("Add result"));
            Assert.That(result, Does.Contain("13"));
            Assert.That(result, Is.Not.Empty);
        });
    }

    [Test]
    public void DivideByZeroThrowsException()
    {
        var exception = Assert.Throws<DivideByZeroException>(() => Calculator.Divide(10, 0));

        Assert.That(exception.Message, Is.EqualTo("Division by zero is prohibited."));
    }

    [TestCaseSource(typeof(CalculatorTestData), nameof(CalculatorTestData.MultiplicationResults))]
    public void GetMultiplicationResultsForDifferentNumbers(int number, List<int> expectedResults)
    {
        var result = Calculator.GetMultiplicationResults(number);

        Assert.That(result, Is.EqualTo(expectedResults));
    }
}
