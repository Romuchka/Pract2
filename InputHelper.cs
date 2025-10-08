using System;
using System.Text.RegularExpressions;

namespace Pract_2
{
    // Вспомогательный класс для проверки безопасного ввода
    public static class InputHelper
    {
        public static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    int value = int.Parse(Console.ReadLine());
                    if (value <= 0)
                    {
                        Console.WriteLine("Ошибка: число должно быть положительным. Попробуйте снова.");
                        continue;
                    }
                    return value;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите целое число. Попробуйте снова.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число слишком большое. Попробуйте снова.");
                }
            }
        }

        public static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    double value = double.Parse(Console.ReadLine());
                    if (value <= 0)
                    {
                        Console.WriteLine("Ошибка: число должно быть положительным. Попробуйте снова.");
                        continue;
                    }
                    return value;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите число. Попробуйте снова.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число слишком большое. Попробуйте снова.");
                }
            }
        }

        public static FlowerType ReadFlowerType(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine("0 - Rose");
                Console.WriteLine("1 - Tulip");
                Console.WriteLine("2 - Daisy");
                Console.WriteLine("3 - Orchid");

                try
                {
                    int choice = ReadInt("Выберите тип цветка (0-3): ");
                    if (choice >= 0 && choice <= 3)
                    {
                        return (FlowerType)choice;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введите число от 0 до 3.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте снова.");
                }
            }
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите целое число. Попробуйте снова.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число слишком большое. Попробуйте снова.");
                }
            }
        }

        public static string ReadColor(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: цвет не может быть пустым. Попробуйте снова.");
                    continue;
                }

                // Проверка, что в цвете только буквы и пробелы
                if (!Regex.IsMatch(input, @"^[a-zA-Zа-яА-ЯёЁ\s]+$"))
                {
                    Console.WriteLine("Ошибка: цвет должен содержать только буквы и пробелы. Попробуйте снова.");
                    continue;
                }

                return input.Trim();
            }
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым. Попробуйте снова.");
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input.Trim();
        }
    }
}