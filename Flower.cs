using System;
using System.Text.RegularExpressions;

namespace Pract_2
{
    // Класс Flower с дополнительными требованиями
    public class Flower : Plant
    {
        private static readonly string CareGuide = "Поливать утром и вечером."; // поле только для чтения
        private int _quantity; // private поле для количества
        private string _color; // private поле для цвета

        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Количество цветков не может быть отрицательным");
                _quantity = value;
            }
        }

        // Конструкторы
        public Flower() { }

        public Flower(Size size, FlowerType type, int quantity, string color)
        {
            Size = size;
            Type = type;
            Quantity = quantity;
            Color = color; // Используем свойство для проверки
        }

        // Свойство для цвета с проверкой
        public string Color
        {
            get => _color;
            set
            {
                if (!IsValidColor(value))
                    throw new ArgumentException("Цвет должен содержать только буквы и пробелы");
                _color = value;
            }
        }

        // Метод проверки цвета (только буквы и пробелы)
        private bool IsValidColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return false;

            return Regex.IsMatch(color, @"^[a-zA-Zа-яА-ЯёЁ\s]+$");
        }

        // Методы
        public void Water()
        {
            Console.WriteLine($"Цветок {Type} полит.");
        }

        public void Smell()
        {
            Console.WriteLine($"Вы вдыхаете аромат {Type}.");
        }

        public void CutFlower()
        {
            if (Quantity > 0)
            {
                Quantity--;
                Console.WriteLine($"Один цветок {Type} срезан. Осталось: {Quantity}");
            }
            else
            {
                Console.WriteLine("Нет цветков для срезания.");
            }
        }

        public static void HowToCare()
        {
            Console.WriteLine(CareGuide);
        }

        // Перегрузка ToString()
        public override string ToString()
        {
            return $"Цветок: {Type}, Размер: {Size}, Количество: {Quantity}, Цвет: {_color}";
        }

        // Перегрузка оператора +
        public static Flower operator +(Flower f, int add)
        {
            return new Flower(f.Size, f.Type, f.Quantity + add, f._color);
        }

        // Перегрузка оператора =
        public static Flower operator +(Flower f1, Flower f2)
        {
            return new Flower(f1.Size, f1.Type, f1.Quantity + f2.Quantity, f1._color);
        }
    }
}