// Console.WriteLine — команда "напечатай строку на экране и перейди на новую строку"
Console.WriteLine("Выберите задание (1, 2, 3 или 4):");
Console.WriteLine("  1 - Калькулятор");
Console.WriteLine("  2 - Числовые промежутки");
Console.WriteLine("  3 - Переводчик погоды");
Console.WriteLine("  4 - Проверка чётности");

// Console.Write — то же самое, но курсор ОСТАЁТСЯ на той же строке (без переноса)
Console.Write("> ");

// Console.ReadLine() — ждёт, пока пользователь что-то напечатает и нажмёт Enter
// string — тип данных "текст" (строка символов)
// ?? "" — если пользователь ничего не ввёл (вернулся null), используем пустую строку
string task = Console.ReadLine() ?? "";

Console.WriteLine();

// switch — "переключатель". Смотрит на значение переменной task
// и выполняет только тот блок case, который совпадает со значением
switch (task)
{
    // ╔══════════════════════════════════════════════╗
    // ║           ЗАДАНИЕ 1: Калькулятор             ║
    // ╚══════════════════════════════════════════════╝
    case "1":
        // double — тип числа с дробной частью (например 3.14 или 10.0)
        // Создаём две переменные-"ящика" и кладём в них числа
        double operand1 = 10;
        double operand2 = 5;

        Console.WriteLine("=== Консольный калькулятор ===");

        // $ перед кавычками — "интерполяция строк"
        // Всё внутри {} заменяется реальным значением переменной при выводе
        Console.WriteLine($"Первое число (operand1) = {operand1}");
        Console.WriteLine($"Второе число (operand2) = {operand2}");
        Console.WriteLine();
        Console.WriteLine("Введите знак арифметической операции (+, -, *, /):");

        // Читаем знак операции, который введёт пользователь
        string sign = Console.ReadLine() ?? "";

        // Вложенный switch — проверяем, какой знак ввели
        switch (sign)
        {
            case "+":
                // Считаем прямо внутри строки — удобно!
                Console.WriteLine($"{operand1} + {operand2} = {operand1 + operand2}");
                break; // break — обязательно! Говорит: "всё, выходи из switch"

            case "-":
                Console.WriteLine($"{operand1} - {operand2} = {operand1 - operand2}");
                break;

            case "*":
                Console.WriteLine($"{operand1} * {operand2} = {operand1 * operand2}");
                break;

            case "/":
                // Математическое правило: на ноль делить нельзя
                // if — "если". Проверяем operand2 перед делением
                if (operand2 == 0)
                    Console.WriteLine("Ошибка! На ноль делить нельзя.");
                else
                    Console.WriteLine($"{operand1} / {operand2} = {operand1 / operand2}");
                break;

            // default — "по умолчанию": срабатывает если ни один case не подошёл
            default:
                Console.WriteLine($"Неизвестный знак: '{sign}'. Используйте +, -, *, /");
                break;
        }
        break; // выходим из case "1" внешнего switch

    // ╔══════════════════════════════════════════════╗
    // ║        ЗАДАНИЕ 2: Числовые промежутки        ║
    // ╚══════════════════════════════════════════════╝
    case "2":
        Console.WriteLine("=== Числовые промежутки ===");
        Console.WriteLine("Введите число от 0 до 100:");

        // Читаем ввод — пока это просто текст, не число
        string input2 = Console.ReadLine() ?? "";

        // int.TryParse — пытается преобразовать текст в целое число (int)
        // Возвращает true если получилось, false если нет (например ввели "abc")
        // out int number2 — куда положить результат, если получилось
        // ! перед if означает "НЕ" — т.е. "если НЕ получилось преобразовать"
        if (!int.TryParse(input2, out int number2))
        {
            Console.WriteLine("Ошибка! Введите целое число.");
        }
        else
        {
            // case int n when ... — это case с условием
            // "int n" — называем текущее число именем n
            // "when" — добавляет дополнительное условие для этого case
            // && означает "И" — оба условия должны выполняться одновременно
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
                // Число отрицательное или больше 100 — ни один case не сработал
                default:
                    Console.WriteLine($"Число {number2} не входит ни в один промежуток [0-100].");
                    break;
            }
        }
        break;

    // ╔══════════════════════════════════════════════╗
    // ║        ЗАДАНИЕ 3: Переводчик погоды          ║
    // ╚══════════════════════════════════════════════╝
    case "3":
        Console.WriteLine("=== Переводчик: Русский → Английский ===");
        Console.WriteLine("Словарь содержит 10 слов о погоде.");
        Console.WriteLine("Введите слово на русском:");

        // Читаем слово и сразу применяем два метода:
        // .ToLower() — делает все буквы строчными: "Дождь" → "дождь", "ДОЖДЬ" → "дождь"
        // .Trim()    — убирает лишние пробелы по краям: "  дождь  " → "дождь"
        // Это нужно чтобы пользователь мог писать как угодно — программа всё равно найдёт слово
        string word = (Console.ReadLine() ?? "").ToLower().Trim();

        // Сравниваем введённое слово с каждым словом в нашем словаре
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
            // Введённого слова нет ни в одном case — значит его нет в словаре
            default:
                Console.WriteLine($"Слово \"{word}\" отсутствует в словаре.");
                break;
        }
        break;

    // ╔══════════════════════════════════════════════╗
    // ║        ЗАДАНИЕ 4: Проверка чётности          ║
    // ╚══════════════════════════════════════════════╝
    case "4":
        Console.WriteLine("=== Проверка чётности ===");
        Console.WriteLine("Введите целое число:");

        string input4 = Console.ReadLine() ?? "";

        // Снова превращаем текст в целое число (int — без дробной части)
        if (!int.TryParse(input4, out int number4))
        {
            Console.WriteLine("Ошибка! Введите целое число.");
        }
        else
        {
            // Оператор % — остаток от деления
            // Примеры: 8 % 2 = 0 → чётное (делится без остатка)
            //          7 % 2 = 1 → нечётное (остаток 1)
            //          0 % 2 = 0 → чётное
            switch (number4)
            {
                // Если остаток от деления на 2 равен нулю — число чётное
                case int n when n % 2 == 0:
                    Console.WriteLine($"Число {n} — чётное.");
                    break;
                // Все остальные числа (остаток 1) — нечётные
                default:
                    Console.WriteLine($"Число {number4} — нечётное.");
                    break;
            }
        }
        break;

    // Пользователь ввёл что-то кроме 1, 2, 3, 4
    default:
        Console.WriteLine("Неверный номер задания. Введите 1, 2, 3 или 4.");
        break;
}
