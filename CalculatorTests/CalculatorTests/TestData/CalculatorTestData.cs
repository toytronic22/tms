namespace CalculatorTests.TestData;

public static class CalculatorTestData
{
    public static IEnumerable<TestCaseData> MultiplicationResults()
    {
        yield return new TestCaseData(2, new List<int> { 2, 4, 6, 8 });
        yield return new TestCaseData(5, new List<int> { 5, 10, 15, 20 });
        yield return new TestCaseData(0, new List<int> { 0, 0, 0, 0 });
        yield return new TestCaseData(-3, new List<int> { -3, -6, -9, -12 });
    }
}
