using System;
using System.Collections.Generic;

namespace Pract_2
{
    // Класс Garden
    public class Garden : IGarden
    {
        private List<Plant> plants = new List<Plant>();

        public void AddPlant(Plant plant)
        {
            plants.Add(plant);
        }

        public void ShowPlants()
        {
            Console.WriteLine("Растения в саду:");
            foreach (var plant in plants)
            {
                if (plant is Flower f)
                    Console.WriteLine(f.ToString());
                else
                    Console.WriteLine($"Куст: {plant.Type}, Размер: {plant.Size}");
            }
        }
    }
}