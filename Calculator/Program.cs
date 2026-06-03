
Console.WriteLine("=== ДЗ #4: Массивы ===");
Console.WriteLine("Выберите задание (1-5):");
Console.WriteLine("  1 — Поиск числа в массиве");
Console.WriteLine("  2 — Удаление всех вхождений числа из массива");
Console.WriteLine("  3 — Случайный массив: максимум, минимум, среднее");
Console.WriteLine("  4 — Двумерный массив: сумма всех элементов");
Console.WriteLine("  5 — Двумерный массив: вывод диагоналей");
Console.Write("> ");

string task = Console.ReadLine() ?? "";
Console.WriteLine();

switch (task)
{
    case "1":
        Console.WriteLine("=== Задание 1: Поиск числа в массиве ===");
        Console.WriteLine();

        int[] arrayTask1 = { 3, 7, 12, 5, 9, 1, 42, 17, 8, 25 };

        Console.WriteLine($"Наш массив: [ {string.Join(", ", arrayTask1)} ]");
        Console.WriteLine();

        Console.Write("Введите число для поиска: ");
        string inputTask1 = Console.ReadLine() ?? "";

        if (!int.TryParse(inputTask1, out int target1))
        {
            Console.WriteLine("Ошибка: введите целое число!");
            break;
        }


        bool found1 = false;

        for (int i = 0; i < arrayTask1.Length; i++)
        {
            if (arrayTask1[i] == target1)
            {
                found1 = true;
                break;
            }
        }

        if (found1)
            Console.WriteLine($"Число {target1} ЕСТЬ в массиве.");
        else
            Console.WriteLine($"Числа {target1} в массиве НЕТ.");

        break;

    case "2":
        Console.WriteLine("=== Задание 2: Удаление числа из массива ===");
        Console.WriteLine();

        int[] arrayTask2 = { 4, 8, 4, 15, 16, 4, 23, 42, 8, 4 };
        Console.WriteLine($"Исходный массив: [ {string.Join(", ", arrayTask2)} ]");
        Console.WriteLine();

        Console.Write("Введите число для удаления: ");
        string inputTask2 = Console.ReadLine() ?? "";

        if (!int.TryParse(inputTask2, out int target2))
        {
            Console.WriteLine("Ошибка: введите целое число!");
            break;
        }

        int countFound = 0;
        for (int i = 0; i < arrayTask2.Length; i++)
        {
            if (arrayTask2[i] == target2)
                countFound++;
        }

        if (countFound == 0)
        {
            Console.WriteLine($"Числа {target2} в массиве нет — удалять нечего.");
            break;
        }

        int newSize = arrayTask2.Length - countFound;
        int[] resultTask2 = new int[newSize];

        int j = 0;
        for (int i = 0; i < arrayTask2.Length; i++)
        {
            if (arrayTask2[i] != target2)
            {
                resultTask2[j] = arrayTask2[i];
                j++;
            }
        }

        Console.WriteLine($"Удалено вхождений числа {target2}: {countFound}");
        Console.WriteLine($"Новый массив: [ {string.Join(", ", resultTask2)} ]");

        break;

    case "3":
        Console.WriteLine("=== Задание 3: Случайный массив ===");
        Console.WriteLine();

        Console.Write("Введите размер массива: ");
        string inputSize = Console.ReadLine() ?? "";

        if (!int.TryParse(inputSize, out int size3) || size3 <= 0)
        {
            Console.WriteLine("Ошибка: введите целое положительное число!");
            break;
        }

        int[] arrayTask3 = new int[size3];

        Random rnd = new Random();

        for (int i = 0; i < arrayTask3.Length; i++)
        {
            arrayTask3[i] = rnd.Next(1, 101);
        }

        Console.WriteLine($"Массив ({size3} эл.): [ {string.Join(", ", arrayTask3)} ]");
        Console.WriteLine();

        int max3 = arrayTask3[0];
        int min3 = arrayTask3[0];
        long sum3 = 0;

        for (int i = 0; i < arrayTask3.Length; i++)
        {
            if (arrayTask3[i] > max3) max3 = arrayTask3[i];
            if (arrayTask3[i] < min3) min3 = arrayTask3[i];
            sum3 += arrayTask3[i];
        }

        double avg3 = (double)sum3 / arrayTask3.Length;

        Console.WriteLine($"Максимальное: {max3}");
        Console.WriteLine($"Минимальное:  {min3}");
        Console.WriteLine($"Среднее:      {avg3:F2}");

        break;

    case "4":
        Console.WriteLine("=== Задание 4: Двумерный массив — сумма ===");
        Console.WriteLine();


        int[,] matrix4 =
        {
            {  1,  2,  3,  4 },
            {  5,  6,  7,  8 },
            {  9, 10, 11, 12 }
        };

        int rows4 = matrix4.GetLength(0);
        int cols4 = matrix4.GetLength(1);

        Console.WriteLine("Матрица:");
        for (int i = 0; i < rows4; i++)
        {
            Console.Write("  [ ");
            for (int k = 0; k < cols4; k++)
            {
                Console.Write($"{matrix4[i, k],4}");
            }
            Console.WriteLine(" ]");
        }
        Console.WriteLine();

        long sum4 = 0;
        for (int i = 0; i < rows4; i++)
            for (int k = 0; k < cols4; k++)
                sum4 += matrix4[i, k];

        Console.WriteLine($"Сумма всех элементов: {sum4}");

        break;

    case "5":
        Console.WriteLine("=== Задание 5: Двумерный массив — диагонали ===");
        Console.WriteLine();


        int[,] matrix5 =
        {
            {  1,  2,  3,  4 },
            {  5,  6,  7,  8 },
            {  9, 10, 11, 12 },
            { 13, 14, 15, 16 }
        };

        int size5 = matrix5.GetLength(0);

        Console.WriteLine("Матрица:");
        for (int i = 0; i < size5; i++)
        {
            Console.Write("  [ ");
            for (int k = 0; k < size5; k++)
                Console.Write($"{matrix5[i, k],4}");
            Console.WriteLine(" ]");
        }
        Console.WriteLine();

        Console.Write("Главная диагональ (вниз-вправо): [ ");
        for (int i = 0; i < size5; i++)
            Console.Write($"{matrix5[i, i],4} ");
        Console.WriteLine("]");

        Console.Write("Побочная диагональ (вниз-влево): [ ");
        for (int i = 0; i < size5; i++)
        {
            int col = (size5 - 1) - i;
            Console.Write($"{matrix5[i, col],4} ");
        }
        Console.WriteLine("]");

        break;

    default:
        Console.WriteLine($"Неверный выбор: '{task}'. Введите число от 1 до 5.");
        break;
}
