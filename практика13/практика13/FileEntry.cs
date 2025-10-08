using System;

namespace NortonCommander
{
    public class FileEntry : FileSystemEntry
    {
        public long Size { get; private set; }
        public string Extension { get; private set; }

        public FileEntry(string name, string extension, long size, DateTime modifiedDate)
            : base(name, modifiedDate, false)
        {
            Size = size;
            Extension = extension ?? "";
        }
        public FileEntry(string name, string extension, DateTime modifiedDate)
             : this(name, extension, 0L, modifiedDate) { }

        public FileEntry(string name, string extension)
            : this(name, extension, 0L, DateTime.Now) { }


        public override void Display(int x, int y, int maxLength)
        {
            if (!IsPositionValid(x, y)) return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y);
            Console.Write(GetFullDisplayName(maxLength));
        }

        // Имя + расширение с сокращением и '~'
        public string GetFullDisplayName(int maxLength)
        {
            string full = string.IsNullOrEmpty(Extension) ? Name : $"{Name}.{Extension}";
            if (full.Length <= maxLength) return full.PadRight(maxLength);

            int dot = full.LastIndexOf('.');
            if (dot > 0)
            {
                string ext = full.Substring(dot); // с точкой
                int keep = Math.Max(1, maxLength - ext.Length - 1);
                return (full.Substring(0, keep) + "~" + ext).PadRight(maxLength);
            }
            return (full.Substring(0, maxLength - 1) + "~").PadRight(maxLength);
        }

        private static bool IsPositionValid(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            if (x >= Console.WindowWidth || y >= Console.WindowHeight) return false;
            return true;
        }
    }
}
