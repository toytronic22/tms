// ШАГ 1: Создаём две переменные с числами
// double - это тип для чисел с запятой (например: 3.14, 10.5)
// Мы выбрали double, чтобы деление тоже давало дробные числа
double operand1 = 10;
double operand2 = 5;

// ШАГ 2: Показываем пользователю числа и просим ввести знак операции
Console.WriteLine("=== Консольный калькулятор ===");
Console.WriteLine($"Первое число (operand1) = {operand1}");
Console.WriteLine($"Второе число (operand2) = {operand2}");
Console.WriteLine();
Console.WriteLine("Введите знак арифметической операции (+, -, *, /):");

// ШАГ 3: Читаем то, что ввёл пользователь, и кладём в переменную sign
// ?? "" означает: "если пользователь ничего не ввёл — считай пустой строкой"
string sign = Console.ReadLine() ?? "";

// ШАГ 4: Используем switch — это как список "если ввёл это — делай то"
switch (sign)
{
    case "+":
        double resultAdd = operand1 + operand2;
        Console.WriteLine($"{operand1} + {operand2} = {resultAdd}");
        break;

    case "-":
        double resultSub = operand1 - operand2;
        Console.WriteLine($"{operand1} - {operand2} = {resultSub}");
        break;

    case "*":
        double resultMul = operand1 * operand2;
        Console.WriteLine($"{operand1} * {operand2} = {resultMul}");
        break;

    case "/":
        // На ноль делить нельзя! Это ошибка в математике.
        if (operand2 == 0)
        {
            Console.WriteLine("Ошибка! На ноль делить нельзя.");
        }
        else
        {
            double resultDiv = operand1 / operand2;
            Console.WriteLine($"{operand1} / {operand2} = {resultDiv}");
        }
        break;

    default:
        Console.WriteLine($"Неизвестный знак: '{sign}'. Используйте +, -, *, /");
        break;
}
