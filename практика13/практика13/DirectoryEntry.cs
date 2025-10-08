using System;

namespace NortonCommander
{
    public class DirectoryEntry : FileSystemEntry
    {
        public int ItemsCount { get; private set; }

        public DirectoryEntry(string name, int itemsCount, DateTime modifiedDate)
            : base(name, modifiedDate, true)
        {
            ItemsCount = itemsCount;
        }

        public override void Display(int x, int y, int maxLength)
        {
            if (!IsPositionValid(x, y)) return;
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y);
            Console.Write(GetShortName(maxLength));
            Console.ForegroundColor = prev;
        }

        private static bool IsPositionValid(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            if (x >= Console.WindowWidth || y >= Console.WindowHeight) return false;
            return true;
        }
    }
}
