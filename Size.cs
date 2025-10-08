using System;

namespace Pract_2
{
    // Структура для размера цветка
    public struct Size
    {
        public double Centimeters { get; }

        public Size(double cm)
        {
            if (cm <= 0)
                throw new ArgumentException("Размер должен быть положительным числом");
            Centimeters = cm;
        }

        public override string ToString()
        {
            return $"{Centimeters} см";
        }
    }
}