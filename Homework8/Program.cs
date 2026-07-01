using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("===== ДЗ #8. Исключения =====");

Task1();
Task2();

// =============================================================================
// ЗАДАНИЕ 1: Калькулятор с обработкой исключений
// =============================================================================

static void Task1()
{
    Console.WriteLine();
    Console.WriteLine("Задание 1 — Калькулятор");

    // Тестируем разные сценарии
    string[] expressions =
    {
        "5 + 3",       // корректное выражение
        "10 / 2",      // корректное выражение
        "7 * 4",       // корректное выражение
        "15 - 8",      // корректное выражение
        "",            // пустая строка -> ArgumentException
        "abc + 3",     // неверный формат -> FormatException
        "5 ^ 3",       // неизвестная операция -> NotSupportedException
        "9 / 0",       // деление на ноль -> DivideByZeroException
        "99999999999999999999 + 1", // слишком большое число -> OverflowException
    };

    foreach (string expr in expressions)
    {
        try
        {
            double result = Calculate(expr);
            Console.WriteLine($"  \"{expr}\" = {result}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ArgumentException: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"  FormatException: {ex.Message}");
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"  NotSupportedException: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"  DivideByZeroException: {ex.Message}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"  OverflowException: {ex.Message}");
        }
        catch (CalculationException ex)
        {
            Console.WriteLine($"  CalculationException: {ex.Message}");
        }
    }
}

// -----------------------------------------------------------------------------
// Метод Calculate: принимает строку вида "5 + 3", возвращает double
// -----------------------------------------------------------------------------
static double Calculate(string expression)
{
    // Проверка на пустую строку
    if (string.IsNullOrWhiteSpace(expression))
        throw new ArgumentException("Выражение не может быть пустым.");

    // Разбиваем строку на части по пробелу
    // "5 + 3" -> ["5", "+", "3"]
    string[] parts = expression.Trim().Split(' ');

    if (parts.Length != 3)
        throw new FormatException($"Неверный формат выражения: \"{expression}\". Ожидается: \"число оператор число\".");

    // Пробуем распознать числа — long.Parse выбрасывает OverflowException
    // если число слишком большое (> ~9.2 * 10^18), и FormatException если это не число.
    long left;
    long right;

    try
    {
        left = long.Parse(parts[0]);
    }
    catch (OverflowException)
    {
        throw new OverflowException($"Число \"{parts[0]}\" слишком большое.");
    }
    catch (Exception)
    {
        throw new FormatException($"Не удалось распознать число: \"{parts[0]}\".");
    }

    try
    {
        right = long.Parse(parts[2]);
    }
    catch (OverflowException)
    {
        throw new OverflowException($"Число \"{parts[2]}\" слишком большое.");
    }
    catch (Exception)
    {
        throw new FormatException($"Не удалось распознать число: \"{parts[2]}\".");
    }

    string op = parts[1];

    // Выполняем операцию
    try
    {
        return op switch
        {
            "+" => (double)(left + right),
            "-" => (double)(left - right),
            "*" => (double)(left * right),
            "/" => right == 0
                ? throw new DivideByZeroException("Деление на ноль невозможно.")
                : (double)left / right,
            _ => throw new NotSupportedException($"Операция \"{op}\" не поддерживается. Используйте: +, -, *, /.")
        };
    }
    catch (DivideByZeroException)
    {
        throw;
    }
    catch (NotSupportedException)
    {
        throw;
    }
    catch (OverflowException)
    {
        throw new OverflowException("Результат вычисления слишком большой.");
    }
    catch (Exception ex)
    {
        // Любая другая ошибка вычисления — оборачиваем в CalculationException
        throw new CalculationException($"Ошибка вычисления: {ex.Message}", ex);
    }
}

// =============================================================================
// ЗАДАНИЕ 2: Валидатор пользователя
// =============================================================================

static void Task2()
{
    Console.WriteLine();
    Console.WriteLine("Задание 2 — Валидатор пользователя");

    var validator = new UserValidator();

    // --- Тесты email ---
    Console.WriteLine();
    Console.WriteLine("  Проверка email:");

    string[] emails =
    {
        "user@example.com",   // корректный
        "",                   // пустой -> ArgumentException
        "userexample.com",    // нет @ -> FormatException
        "user@examplecom",    // нет . после @ -> FormatException
        "u@e.c",              // длина <= 5 -> FormatException
    };

    foreach (string email in emails)
    {
        try
        {
            validator.ValidateEmail(email);
            Console.WriteLine($"    \"{email}\" — корректный ✓");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"    ArgumentException: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"    FormatException: {ex.Message}");
        }
    }

    // --- Тесты пароля ---
    Console.WriteLine();
    Console.WriteLine("  Проверка пароля:");

    string[] passwords =
    {
        "Secret1!",       // корректный
        "",               // пустой -> ArgumentException
        "weakpass",       // нет цифр, нет заглавных, нет спецсимволов -> WeakPasswordException
        "NoDigits!A",     // нет цифры -> WeakPasswordException
        "nouppercase1!",  // нет заглавной -> WeakPasswordException
        "NoSpecial1A",    // нет спецсимвола -> WeakPasswordException
    };

    foreach (string password in passwords)
    {
        try
        {
            validator.ValidatePassword(password);
            Console.WriteLine($"    \"{password}\" — корректный ✓");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"    ArgumentException: {ex.Message}");
        }
        catch (WeakPasswordException ex)
        {
            Console.WriteLine($"    WeakPasswordException: {ex.Message}");
        }
    }
}

// =============================================================================
// Классы исключений и UserValidator
// =============================================================================

/// <summary>
/// Своё исключение для ошибок вычислений.
/// </summary>
class CalculationException : Exception
{
    public CalculationException(string message) : base(message) { }
    public CalculationException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Своё исключение для слабого пароля.
/// </summary>
class WeakPasswordException : Exception
{
    public WeakPasswordException(string message) : base(message) { }
}

/// <summary>
/// Валидатор email и пароля пользователя.
/// </summary>
class UserValidator
{
    // Специальные символы, которые обязаны присутствовать в пароле
    private const string SpecialChars = "!@#$%^&*";

    /// <summary>
    /// Проверяет корректность email.
    /// Выбрасывает ArgumentException или FormatException при ошибке.
    /// </summary>
    public void ValidateEmail(string email)
    {
        // Проверка: не пустой
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email не может быть пустым.");

        // Проверка: длина > 5
        if (email.Length <= 5)
            throw new FormatException($"Email \"{email}\" слишком короткий (минимум 6 символов).");

        // Проверка: содержит @
        int atIndex = email.IndexOf('@');
        if (atIndex < 0)
            throw new FormatException($"Email \"{email}\" не содержит символ @.");

        // Проверка: содержит . после @
        string afterAt = email.Substring(atIndex + 1);
        if (!afterAt.Contains('.'))
            throw new FormatException($"Email \"{email}\" не содержит точку после @.");
    }

    /// <summary>
    /// Проверяет надёжность пароля.
    /// Выбрасывает ArgumentException или WeakPasswordException при ошибке.
    /// </summary>
    public void ValidatePassword(string password)
    {
        // Проверка: не пустой
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Пароль не может быть пустым.");

        // Проверка: длина >= 8
        if (password.Length < 8)
            throw new WeakPasswordException($"Пароль слишком короткий: {password.Length} симв. (минимум 8).");

        // Проверка: есть хотя бы одна заглавная буква
        bool hasUpper = false;
        for (int i = 0; i < password.Length; i++)
        {
            if (char.IsUpper(password[i]))
            {
                hasUpper = true;
                break;
            }
        }
        if (!hasUpper)
            throw new WeakPasswordException("Пароль должен содержать хотя бы одну заглавную букву.");

        // Проверка: есть хотя бы одна цифра
        bool hasDigit = false;
        for (int i = 0; i < password.Length; i++)
        {
            if (char.IsDigit(password[i]))
            {
                hasDigit = true;
                break;
            }
        }
        if (!hasDigit)
            throw new WeakPasswordException("Пароль должен содержать хотя бы одну цифру.");

        // Проверка: есть хотя бы один специальный символ
        bool hasSpecial = false;
        for (int i = 0; i < password.Length; i++)
        {
            if (SpecialChars.Contains(password[i]))
            {
                hasSpecial = true;
                break;
            }
        }
        if (!hasSpecial)
            throw new WeakPasswordException($"Пароль должен содержать хотя бы один специальный символ ({SpecialChars}).");
    }
}





