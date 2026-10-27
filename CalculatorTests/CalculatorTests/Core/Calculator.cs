namespace CalculatorTests.Core;

public class Calculator
{
    public int Add(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public double Divide(double firstNumber, double secondNumber)
    {
        if (secondNumber == 0)
        {
            throw new DivideByZeroException("Division by zero is prohibited.");
        }

        return firstNumber / secondNumber;
    }

    public string GetResultAsString(int firstNumber, int secondNumber)
    {
        return $"Add result: {Add(firstNumber, secondNumber)}";
    }

    public List<int> GetMultiplicationResults(int number)
    {
        return new List<int>
        {
            number * 1,
            number * 2,
            number * 3,
            number * 4
        };
    }
}
