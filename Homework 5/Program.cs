using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("===== ДЗ #5. Строки =====");

Task1();
Task2();

static void Task1()
{
    Console.WriteLine();
    Console.WriteLine("Задание 1");

    string text = "test123 hello test456 world test789";
    Console.WriteLine($"Исходная строка: {text}");

    string replacedText = text.Replace("test", "testing");

    string result = "";

    for (int i = 0; i < replacedText.Length; i++)
    {
        if (!char.IsDigit(replacedText[i]))
        {
            result += replacedText[i];
        }
    }

    Console.WriteLine($"Результат: {result}");
}

static void Task2()
{
    Console.WriteLine();
    Console.WriteLine("Задание 2");

    string text = "teamwithsomeofexcersicesabcwanttomakeitbetter";
    string marker = "abc";

    int index = text.IndexOf(marker);

    string beforeAbc = text.Substring(0, index);
    string afterAbc = text.Substring(index + marker.Length);

    Console.WriteLine($"Исходная строка: {text}");
    Console.WriteLine($"До abc: {beforeAbc}");
    Console.WriteLine($"После abc: {afterAbc}");
}
