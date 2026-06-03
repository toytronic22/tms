Console.WriteLine("Выберите задание (1, 2, 3 или 4):");
Console.WriteLine("  1 - Калькулятор");
Console.WriteLine("  2 - Числовые промежутки");
Console.WriteLine("  3 - Переводчик погоды");
Console.WriteLine("  4 - Проверка чётности");
Console.Write("> ");
string task = Console.ReadLine() ?? "";

Console.WriteLine();
switch (task)
{
    case "1":
        double operand1 = 10;
        double operand2 = 5;

        Console.WriteLine("=== Консольный калькулятор ===");
        Console.WriteLine($"Первое число (operand1) = {operand1}");
        Console.WriteLine($"Второе число (operand2) = {operand2}");
        Console.WriteLine();
        Console.WriteLine("Введите знак арифметической операции (+, -, *, /):");
        string sign = Console.ReadLine() ?? "";

        switch (sign)
        {
            case "+":
                Console.WriteLine($"{operand1} + {operand2} = {operand1 + operand2}");
                break;

            case "-":
                Console.WriteLine($"{operand1} - {operand2} = {operand1 - operand2}");
                break;

            case "*":
                Console.WriteLine($"{operand1} * {operand2} = {operand1 * operand2}");
                break;

            case "/":
                if (operand2 == 0)
                    Console.WriteLine("Ошибка! На ноль делить нельзя.");
                else
                    Console.WriteLine($"{operand1} / {operand2} = {operand1 / operand2}");
                break;

            default:
                Console.WriteLine($"Неизвестный знак: '{sign}'. Используйте +, -, *, /");
                break;
        }
        break;

    case "2":
        Console.WriteLine("=== Числовые промежутки ===");
        Console.WriteLine("Введите число от 0 до 100:");

        string input2 = Console.ReadLine() ?? "";

        if (!int.TryParse(input2, out int number2))
        {
            Console.WriteLine("Ошибка! Введите целое число.");
        }
        else
        {
            switch (number2)
            {
                case int n when n >= 0 && n <= 14:
                    Console.WriteLine($"Число {n} попадает в промежуток [0 - 14]");
                    break;
                case int n when n >= 15 && n <= 35:
                    Console.WriteLine($"Число {n} попадает в промежуток [15 - 35]");
                    break;
                case int n when n >= 36 && n <= 50:
                    Console.WriteLine($"Число {n} попадает в промежуток [36 - 50]");
                    break;
                case int n when n >= 51 && n <= 100:
                    Console.WriteLine($"Число {n} попадает в промежуток [51 - 100]");
                    break;
                default:
                    Console.WriteLine($"Число {number2} не входит ни в один промежуток [0-100].");
                    break;
            }
        }
        break;

    case "3":
        Console.WriteLine("=== Переводчик: Русский -> Английский ===");
        Console.WriteLine("Словарь содержит 10 слов о погоде.");
        Console.WriteLine("Введите слово на русском:");

        string word = (Console.ReadLine() ?? "").ToLower().Trim();

        switch (word)
        {
            case "погода":  Console.WriteLine("weather");      break;
            case "дождь":   Console.WriteLine("rain");         break;
            case "снег":    Console.WriteLine("snow");         break;
            case "солнце":  Console.WriteLine("sun");          break;
            case "ветер":   Console.WriteLine("wind");         break;
            case "туман":   Console.WriteLine("fog");          break;
            case "гроза":   Console.WriteLine("thunderstorm"); break;
            case "мороз":   Console.WriteLine("frost");        break;
            case "метель":  Console.WriteLine("blizzard");     break;
            case "радуга":  Console.WriteLine("rainbow");      break;
            default:
                Console.WriteLine($"Слово \"{word}\" отсутствует в словаре.");
                break;
        }
        break;

    case "4":
        Console.WriteLine("=== Проверка чётности ===");
        Console.WriteLine("Введите целое число:");

        string input4 = Console.ReadLine() ?? "";

        if (!int.TryParse(input4, out int number4))
        {
            Console.WriteLine("Ошибка! Введите целое число.");
        }
        else
        {
            switch (number4)
            {
                case int n when n % 2 == 0:
                    Console.WriteLine($"Число {n} — чётное.");
                    break;
                default:
                    Console.WriteLine($"Число {number4} — нечётное.");
                    break;
            }
        }
        break;

    default:
        Console.WriteLine("Неверный номер задания. Введите 1, 2, 3 или 4.");
        break;
}
