namespace CalculatorTests.Hooks;

public abstract class BaseTest
{
    protected readonly Core.Calculator Calculator = new();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine($"Suite started: {GetType().Name}");
    }

    [SetUp]
    public void SetUp()
    {
        Console.WriteLine($"Test started: {TestContext.CurrentContext.Test.Name}");
    }

    [TearDown]
    public void TearDown()
    {
        Console.WriteLine($"Test finished: {TestContext.CurrentContext.Test.Name} - {TestContext.CurrentContext.Result.Outcome.Status}");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine($"Suite finished: {GetType().Name}");
    }
}
