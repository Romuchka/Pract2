using System;

namespace NortonCommander
{
    public abstract class FileSystemEntry
    {
        public string Name { get; protected set; }
        public DateTime ModifiedDate { get; protected set; }
        public bool IsDirectory { get; protected set; }

        protected FileSystemEntry(string name, DateTime modifiedDate, bool isDirectory)
        {
            Name = name ?? string.Empty;
            ModifiedDate = modifiedDate;
            IsDirectory = isDirectory;
        }

        public abstract void Display(int x, int y, int maxLength);

        // Сокращение имени с сохранением расширения и признаком '~'
        public virtual string GetShortName(int maxLength)
        {
            if (maxLength <= 0) return string.Empty;

            if (Name.Length <= maxLength)
                return Name.PadRight(maxLength);

            int dot = Name.LastIndexOf('.');
            if (dot > 0 && dot < Name.Length - 1)
            {
                string ext = Name.Substring(dot); // включая точку
                int keep = Math.Max(1, maxLength - ext.Length - 1); // 1 под '~'
                return (Name.Substring(0, keep) + "~" + ext).PadRight(maxLength);
            }

            return (Name.Substring(0, maxLength - 1) + "~").PadRight(maxLength);
        }
    }
}
