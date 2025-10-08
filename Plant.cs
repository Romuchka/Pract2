namespace Pract_2
{
    // Базовый класс Plant
    public class Plant
    {
        public Size Size { get; set; }
        public FlowerType Type { get; set; }

        public void Deconstruct(out Size size, out FlowerType type)
        {
            size = Size;
            type = Type;
        }
    }
}