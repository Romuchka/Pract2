using System;
using System.Collections.Generic;
using System.Linq;

namespace Практика2
{
    // Перечисляемый тип для видов цветов
    public enum FlowerType { Rose, Tulip, Daisy, Orchid, Lily, Sunflower }

    // Структура для хранения размера
    public struct Size
    {
        public int Height;
        public int Width;

        public Size(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return $"Высота: {Height}см, Ширина: {Width}см";
        }
    }

    // Базовый класс Plant
    public class Plant
    {
        protected Size size;
        protected FlowerType type;
        private string id;

        // Свойства с разным уровнем доступа
        public Size PlantSize
        {
            get { return size; }
            set { size = value; }
        }

        public FlowerType PlantType
        {
            get { return type; }
            set { type = value; }
        }

        public string ID
        {
            get { return id; }
            private set { id = value; }
        }

        public virtual string Name { get; set; }

        // Константа
        public const string KINGDOM = "Plantae";

        // Readonly поле
        public readonly DateTime CreatedDate;

        // Конструктор по умолчанию
        public Plant()
        {
            size = new Size(0, 0);
            type = FlowerType.Rose;
            id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Name = "Безымянное растение";
        }

        // Конструктор с параметрами
        public Plant(Size plantSize, FlowerType plantType, string name)
        {
            size = plantSize;
            type = plantType;
            id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Name = name;
        }

        // Виртуальный метод
        public virtual void Grow()
        {
            size.Height += 5;
            Console.WriteLine($"{Name} вырос на 5см. Теперь высота: {size.Height}см");
        }

        // Перегрузка ToString
        public override string ToString()
        {
            return $"Растение: {Name}, Тип: {type}, Размер: {size}, Создано: {CreatedDate:dd.MM.yyyy}";
        }

        // Перегрузка операции ==
        public static bool operator ==(Plant plant1, Plant plant2)
        {
            if (ReferenceEquals(plant1, plant2)) return true;
            if (plant1 is null || plant2 is null) return false;
            return plant1.id == plant2.id;
        }

        public static bool operator !=(Plant plant1, Plant plant2)
        {
            return !(plant1 == plant2);
        }

        public override bool Equals(object obj)
        {
            return obj is Plant plant && id == plant.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }

    // Класс Flower, наследующий Plant
    public class Flower : Plant
    {
        private int quantity;
        private bool isBlooming;
        private string color;
        private int waterLevel;

        // Свойства
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value >= 0 ? value : 0; }
        }

        public bool IsBlooming
        {
            get { return isBlooming; }
            private set { isBlooming = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public int WaterLevel
        {
            get { return waterLevel; }
            private set { waterLevel = Math.Max(0, Math.Min(100, value)); }
        }

        // Константа
        public const int MAX_BLOOM_DAYS = 30;

        // Конструкторы
        public Flower() : base()
        {
            Quantity = 1;
            IsBlooming = true;
            Color = "Неизвестный";
            WaterLevel = 50;
        }

        public Flower(Size size, FlowerType type, string name, int quantity, string color)
            : base(size, type, name)
        {
            Quantity = quantity;
            IsBlooming = true;
            Color = color;
            WaterLevel = 50;
        }

        // Методы
        public void GiveWater(int amount = 20)
        {
            WaterLevel += amount;
            Console.WriteLine($"{Name} полили. Уровень воды: {WaterLevel}%");

            if (WaterLevel > 70 && !IsBlooming)
            {
                IsBlooming = true;
                Console.WriteLine($"{Name} начинает цвести!");
            }
        }

        public void Inhale()
        {
            string fragrance = GetFragranceDescription();
            Console.WriteLine($"Вы вдыхаете аромат {Name}. {fragrance}");
        }

        public void CutFlower()
        {
            if (Quantity > 0)
            {
                Quantity--;
                Console.WriteLine($"Вы срезали один цветок {Name}. Осталось: {Quantity}");

                if (Quantity == 0)
                {
                    IsBlooming = false;
                    Console.WriteLine($"Все цветы {Name} срезаны.");
                }
            }
            else
            {
                Console.WriteLine($"Нет цветов {Name} для срезания.");
            }
        }

        // Статический метод
        public static void LearnCare()
        {
            Console.WriteLine("=== Инструкция по уходу за цветами ===");
            Console.WriteLine("1. Поливайте регулярно, но не переливайте");
            Console.WriteLine("2. Обеспечьте достаточное освещение");
            Console.WriteLine("3. Удаляйте увядшие цветы");
            Console.WriteLine("4. Подкармливайте в период роста");
            Console.WriteLine("================================");
        }

        // Перегрузка методов
        public override void Grow()
        {
            base.Grow();
            if (WaterLevel > 30)
            {
                Quantity += 1;
                Console.WriteLine($"{Name} дал новый цветок! Теперь количество: {Quantity}");
            }
        }

        public override string ToString()
        {
            string bloomStatus = IsBlooming ? "цветущий" : "не цветущий";
            return $"Цветок: {Name}, Тип: {PlantType}, Цвет: {Color}, " +
                   $"Количество: {Quantity}, Статус: {bloomStatus}, Уровень воды: {WaterLevel}%";
        }

        // Перегрузка арифметической операции +
        public static Flower operator +(Flower flower, int amount)
        {
            flower.Quantity += amount;
            Console.WriteLine($"Добавлено {amount} цветков к {flower.Name}. Теперь: {flower.Quantity}");
            return flower;
        }

        // Вспомогательный приватный метод
        private string GetFragranceDescription()
        {
            switch (PlantType)
            {
                case FlowerType.Rose: return "Нежный сладкий аромат розы";
                case FlowerType.Tulip: return "Едва уловимый свежий аромат";
                case FlowerType.Daisy: return "Лёгкий травянистый запах";
                case FlowerType.Orchid: return "Экзотический и насыщенный аромат";
                case FlowerType.Lily: return "Сильный сладкий аромат";
                case FlowerType.Sunflower: return "Солнечный аромат полей";
                default: return "Приятный цветочный аромат";
            }
        }
    }

    // Класс Bush, наследующий Plant
    public class Bush : Plant
    {
        private int branchCount;
        private bool isTrimmed;

        public int BranchCount
        {
            get { return branchCount; }
            set { branchCount = value; }
        }

        public bool IsTrimmed
        {
            get { return isTrimmed; }
            private set { isTrimmed = value; }
        }

        public Bush() : base()
        {
            BranchCount = 5;
            IsTrimmed = false;
        }

        public Bush(Size size, FlowerType type, string name, int branches)
            : base(size, type, name)
        {
            BranchCount = branches;
            IsTrimmed = false;
        }

        public void Cut()
        {
            if (BranchCount > 3)
            {
                int cutBranches = BranchCount / 3;
                BranchCount -= cutBranches;
                IsTrimmed = true;
                Console.WriteLine($"Куст {Name} подстрижен. Убрано {cutBranches} веток. Осталось: {BranchCount}");
            }
            else
            {
                Console.WriteLine($"Куст {Name} слишком мал для стрижки.");
            }
        }

        public override void Grow()
        {
            base.Grow();
            BranchCount += 2;
            IsTrimmed = false;
            Console.WriteLine($"У куста {Name} выросло 2 новые ветки. Теперь: {BranchCount} веток");
        }

        public override string ToString()
        {
            string trimStatus = IsTrimmed ? "подстрижен" : "требует стрижки";
            return $"Куст: {Name}, Тип: {PlantType}, Веток: {BranchCount}, Статус: {trimStatus}";
        }
    }

    // Интерфейс IGarden
    public interface IGarden
    {
        void ShowPlants();
    }

    // Класс Garden, реализующий IGarden
    public class Garden : IGarden
    {
        private List<Plant> plants;
        private string gardenName;

        public string GardenName
        {
            get { return gardenName; }
            set { gardenName = value; }
        }

        public int PlantCount
        {
            get { return plants.Count; }
        }

        public Garden(string name)
        {
            gardenName = name;
            plants = new List<Plant>();
        }

        // Реализация метода интерфейса
        public void ShowPlants()
        {
            Console.WriteLine($"\n=== Растения в саду '{GardenName}' ===");
            if (plants.Count == 0)
            {
                Console.WriteLine("В саду пока нет растений.");
                return;
            }

            for (int i = 0; i < plants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plants[i]}");
            }
            Console.WriteLine($"Всего растений: {plants.Count}\n");
        }

        // Методы для управления коллекцией
        public void AddPlant(Plant plant)
        {
            plants.Add(plant);
            Console.WriteLine($"Растение '{plant.Name}' добавлено в сад.");
        }

        public bool RemovePlant(int index)
        {
            if (index >= 0 && index < plants.Count)
            {
                string name = plants[index].Name;
                plants.RemoveAt(index);
                Console.WriteLine($"Растение '{name}' удалено из сада.");
                return true;
            }
            return false;
        }

        public Plant GetPlant(int index)
        {
            if (index >= 0 && index < plants.Count)
                return plants[index];
            return null;
        }

        // Статистика сада
        public void ShowGardenStats()
        {
            int flowersCount = plants.OfType<Flower>().Count();
            int bushesCount = plants.OfType<Bush>().Count();

            Console.WriteLine($"\n=== Статистика сада '{GardenName}' ===");
            Console.WriteLine($"Всего растений: {plants.Count}");
            Console.WriteLine($"Цветов: {flowersCount}");
            Console.WriteLine($"Кустов: {bushesCount}");

            if (flowersCount > 0)
            {
                var bloomingFlowers = plants.OfType<Flower>().Count(f => f.IsBlooming);
                Console.WriteLine($"Цветущих цветов: {bloomingFlowers}");
            }
        }
    }
}