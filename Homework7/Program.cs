using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("===== ДЗ #7. Строки =====");

Task4();

static void Task4()
{
    Console.WriteLine();
    Console.WriteLine("Задание 4");

    // Формат: xxxx-yyy-xxxx-yyy-xyxy
    // x — число, y — буква
    string documentNumber = "5555-AbC-1234-dEf-5g6H";

    Console.WriteLine($"Номер документа: {documentNumber}");

    string[] blocks = documentNumber.Split('-');

    // Вывести два первых блока по 4 цифры.
    Console.WriteLine($"{blocks[0]} {blocks[2]}");

    // Вывести номер документа, но блоки из трех букв заменить на ***.
    string maskedDocument = $"{blocks[0]}-***-{blocks[2]}-***-{blocks[4]}";
    Console.WriteLine(maskedDocument);

    // Вывести только буквы в формате yyy/yyy/y/y в нижнем регистре.
    string lettersLower = $"{blocks[1]}/{blocks[3]}/{blocks[4][1]}/{blocks[4][3]}".ToLower();
    Console.WriteLine(lettersLower);

    // Вывести буквы в формате Letters:yyy/yyy/y/y в верхнем регистре через StringBuilder.
    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.Append("Letters:");
    stringBuilder.Append(blocks[1]);
    stringBuilder.Append("/");
    stringBuilder.Append(blocks[3]);
    stringBuilder.Append("/");
    stringBuilder.Append(blocks[4][1]);
    stringBuilder.Append("/");
    stringBuilder.Append(blocks[4][3]);

    string lettersUpper = stringBuilder.ToString().ToUpper();
    Console.WriteLine(lettersUpper);

    // Проверить, содержит ли номер документа abc без учёта регистра.
    if (documentNumber.ToLower().Contains("abc"))
    {
        Console.WriteLine("Номер документа содержит последовательность abc");
    }
    else
    {
        Console.WriteLine("Номер документа не содержит последовательность abc");
    }

    // Проверить, начинается ли номер документа с 555.
    if (documentNumber.StartsWith("555"))
    {
        Console.WriteLine("Номер документа начинается с 555");
    }
    else
    {
        Console.WriteLine("Номер документа не начинается с 555");
    }
}

