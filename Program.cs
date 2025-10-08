using System;

namespace Pract_2
{
    // Тестирующая программа с меню
    class Program
    {
        static void Main()
        {
            Garden garden = new Garden();
            Flower currentFlower = null;

            while (true)
            {
                Console.WriteLine("\n=== Меню ===");
                Console.WriteLine("1. Создать цветок");
                Console.WriteLine("2. Вывести информацию о цветке");
                Console.WriteLine("3. Полить цветок");
                Console.WriteLine("4. Вдохнуть аромат");
                Console.WriteLine("5. Срезать цветок");
                Console.WriteLine("6. Узнать уход");
                Console.WriteLine("7. Добавить цветок в сад");
                Console.WriteLine("8. Показать растения в саду");
                Console.WriteLine("0. Выход");

                int choice;
                try
                {
                    choice = InputHelper.ReadInt("Выберите пункт меню: ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка ввода: {ex.Message}");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            double size = InputHelper.ReadPositiveDouble("Размер (см): ");
                            FlowerType type = InputHelper.ReadFlowerType("Тип цветка:");
                            int quantity = InputHelper.ReadPositiveInt("Количество: ");
                            string color = InputHelper.ReadColor("Цвет: ");

                            currentFlower = new Flower(new Size(size), type, quantity, color);
                            Console.WriteLine("Цветок успешно создан!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при создании цветка: {ex.Message}");
                        }
                        break;

                    case 2:
                        if (currentFlower != null)
                            Console.WriteLine(currentFlower);
                        else
                            Console.WriteLine("Цветок не создан.");
                        break;

                    case 3:
                        if (currentFlower != null)
                            currentFlower.Water();
                        else
                            Console.WriteLine("Сначала создайте цветок.");
                        break;

                    case 4:
                        if (currentFlower != null)
                            currentFlower.Smell();
                        else
                            Console.WriteLine("Сначала создайте цветок.");
                        break;

                    case 5:
                        if (currentFlower != null)
                            currentFlower.CutFlower();
                        else
                            Console.WriteLine("Сначала создайте цветок.");
                        break;

                    case 6:
                        Flower.HowToCare();
                        break;

                    case 7:
                        if (currentFlower != null)
                        {
                            garden.AddPlant(currentFlower);
                            Console.WriteLine("Цветок добавлен в сад.");
                        }
                        else
                        {
                            Console.WriteLine("Сначала создайте цветок.");
                        }
                        break;

                    case 8:
                        garden.ShowPlants();
                        break;

                    case 0:
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Неверный пункт меню. Введите число от 0 до 8.");
                        break;
                }
            }
        }
    }
}