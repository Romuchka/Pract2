using System;
using System.Collections.Generic;

namespace Практика2
{
    class Program
    {
        private static Garden currentGarden;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Создаем сад по умолчанию
            currentGarden = new Garden("Мой прекрасный сад");

            bool exit = false;
            while (!exit)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewPlant();
                        break;
                    case "2":
                        currentGarden.ShowPlants();
                        break;
                    case "3":
                        TestPlantMethods();
                        break;
                    case "4":
                        ShowGardenStats();
                        break;
                    case "5":
                        RemovePlant();
                        break;
                    case "6":
                        Flower.LearnCare();
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("До свидания! Хорошего дня в саду!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ САДОМ ===");
            Console.WriteLine($"Текущий сад: {currentGarden.GardenName}");
            Console.WriteLine($"Растений в саду: {currentGarden.PlantCount}");
            Console.WriteLine("================================");
            Console.WriteLine("1. Добавить новое растение");
            Console.WriteLine("2. Показать все растения");
            Console.WriteLine("3. Тестировать методы растения");
            Console.WriteLine("4. Статистика сада");
            Console.WriteLine("5. Удалить растение");
            Console.WriteLine("6. Инструкция по уходу за цветами");
            Console.WriteLine("7. Выход");
            Console.WriteLine("================================");
            Console.Write("Выберите опцию: ");
        }

        static void CreateNewPlant()
        {
            Console.Clear();
            Console.WriteLine("=== СОЗДАНИЕ НОВОГО РАСТЕНИЯ ===");
            Console.WriteLine("1. Создать цветок");
            Console.WriteLine("2. Создать куст");
            Console.Write("Выберите тип растения: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateFlower();
                    break;
                case "2":
                    CreateBush();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        static void CreateFlower()
        {
            Console.WriteLine("\n--- Создание нового цветка ---");

            Console.Write("Введите название цветка: ");
            string name = Console.ReadLine();

            Console.WriteLine("Доступные типы цветов:");
            int typeIndex = 1;
            foreach (FlowerType type in Enum.GetValues(typeof(FlowerType)))
            {
                Console.WriteLine($"{typeIndex}. {type}");
                typeIndex++;
            }
            Console.Write("Выберите тип цветка (1-6): ");
            FlowerType flowerType = (FlowerType)(int.Parse(Console.ReadLine()) - 1);

            Console.Write("Введите высоту цветка (см): ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Введите ширину цветка (см): ");
            int width = int.Parse(Console.ReadLine());

            Console.Write("Введите количество цветков: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Введите цвет: ");
            string color = Console.ReadLine();

            Size size = new Size(height, width);
            Flower newFlower = new Flower(size, flowerType, name, quantity, color);
            currentGarden.AddPlant(newFlower);
        }

        static void CreateBush()
        {
            Console.WriteLine("\n--- Создание нового куста ---");

            Console.Write("Введите название куста: ");
            string name = Console.ReadLine();

            Console.WriteLine("Доступные типы:");
            int typeIndex = 1;
            foreach (FlowerType type in Enum.GetValues(typeof(FlowerType)))
            {
                Console.WriteLine($"{typeIndex}. {type}");
                typeIndex++;
            }
            Console.Write("Выберите тип куста (1-6): ");
            FlowerType bushType = (FlowerType)(int.Parse(Console.ReadLine()) - 1);

            Console.Write("Введите высоту куста (см): ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Введите ширину куста (см): ");
            int width = int.Parse(Console.ReadLine());

            Console.Write("Введите количество веток: ");
            int branches = int.Parse(Console.ReadLine());

            Size size = new Size(height, width);
            Bush newBush = new Bush(size, bushType, name, branches);
            currentGarden.AddPlant(newBush);
        }

        static void TestPlantMethods()
        {
            if (currentGarden.PlantCount == 0)
            {
                Console.WriteLine("В саду нет растений для тестирования.");
                return;
            }

            currentGarden.ShowPlants();
            Console.Write("Выберите номер растения для тестирования: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            Plant selectedPlant = currentGarden.GetPlant(index);
            if (selectedPlant == null)
            {
                Console.WriteLine("Неверный номер растения.");
                return;
            }

            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine($"=== ТЕСТИРОВАНИЕ: {selectedPlant.Name} ===");
                Console.WriteLine($"Тип: {selectedPlant.GetType().Name}");
                Console.WriteLine("Доступные методы:");

                if (selectedPlant is Flower flower)
                {
                    Console.WriteLine("1. Полить цветок");
                    Console.WriteLine("2. Вдохнуть аромат");
                    Console.WriteLine("3. Срезать цветок");
                    Console.WriteLine("4. Увеличить количество цветков (+)");
                    Console.WriteLine("5. Вырастить");
                }
                else if (selectedPlant is Bush bush)
                {
                    Console.WriteLine("1. Подстричь куст");
                    Console.WriteLine("2. Вырастить");
                }

                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Выберите метод: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (selectedPlant is Flower f1)
                            f1.GiveWater();
                        else if (selectedPlant is Bush b1)
                            b1.Cut();
                        break;
                    case "2":
                        if (selectedPlant is Flower f2)
                            f2.Inhale();
                        else
                            Console.WriteLine("Метод не доступен для этого типа растения.");
                        break;
                    case "3":
                        if (selectedPlant is Flower f3)
                            f3.CutFlower();
                        else
                            Console.WriteLine("Метод не доступен для этого типа растения.");
                        break;
                    case "4":
                        if (selectedPlant is Flower f4)
                        {
                            Console.Write("Введите количество для добавления: ");
                            int amount = int.Parse(Console.ReadLine());
                            f4 += amount;
                        }
                        else
                            Console.WriteLine("Метод не доступен для этого типа растения.");
                        break;
                    case "5":
                        selectedPlant.Grow();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowGardenStats()
        {
            currentGarden.ShowGardenStats();
        }

        static void RemovePlant()
        {
            if (currentGarden.PlantCount == 0)
            {
                Console.WriteLine("В саду нет растений для удаления.");
                return;
            }

            currentGarden.ShowPlants();
            Console.Write("Введите номер растения для удаления: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (!currentGarden.RemovePlant(index))
            {
                Console.WriteLine("Неверный номер растения.");
            }
        }
    }
}