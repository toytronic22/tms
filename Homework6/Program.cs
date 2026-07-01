using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("===== ДЗ #6. Строки =====");

Task3();

static void Task3()
{
    Console.WriteLine();
    Console.WriteLine("Задание 3");

    string text = "Плохой день.";

    // Удаляем слово "Плохой " через Substring.
    string day = text.Substring(7);

    // Через Insert добавляем слово "Хороший " в начало.
    string result = day.Insert(0, "Хороший ");

    // Получаем строку "Хороший день!!!!!!!!!"
    result = result.Replace(".", "!!!!!!!!!");

    // Заменяем последний "!" на "?"
    int lastExclamationIndex = result.LastIndexOf('!');
    result = result.Remove(lastExclamationIndex, 1).Insert(lastExclamationIndex, "?");

    Console.WriteLine(result);
}

