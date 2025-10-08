using System;
using System.Collections.Generic;
using System.Linq;

namespace NortonCommander
{
    public class FileManager
    {
        public List<FileSystemEntry> GetLeftPanelEntries()
        {
            var now = new DateTime(2002, 10, 11, 19, 48, 0);

            var list = new List<FileSystemEntry>
            {
                new DirectoryEntry("..", 0, now),
                new DirectoryEntry("Ajaccgdo\u2593", 3, now),
                new FileEntry("nc", "cfg", now),
                new FileEntry("nc_exit", "com", now),
                new FileEntry("telemax", "dat", now),
                new FileEntry("nc_exit", "doc", now),
                new FileEntry("123view", "exe", now),
                new FileEntry("arcview", "exe", now),
                new FileEntry("bitmap", "exe", now),
                new FileEntry("clp2dib", "exe", now),
                new FileEntry("dbvies", "exe", now),
                new FileEntry("draw2wmf", "exe", now),
                new FileEntry("ico2dib", "exe", now),
                new FileEntry("msp2dib", "exe", now),
                new FileEntry("ncclean", "exe", now),

            };
            while (list.Count < 17)
                list.Add(new FileEntry("", "", now));
            list.AddRange(new List<FileSystemEntry>
            {
                new FileEntry("ncdd",     "exe", now),
                new FileEntry("ncedit",   "exe", now),
                new FileEntry("ncff",     "exe", now),
                new FileEntry("nclabel",  "exe", now),
                new FileEntry("ncmain",   "exe", now),
                new FileEntry("ncnet",    "exe", now),
                new FileEntry("ncsf",     "exe", now),
                new FileEntry("ncsi",     "exe", now),
                new FileEntry("nczip",    "exe", now),
                new FileEntry("paraview", "exe", now),
                new FileEntry("pct2dib",  "exe", now),
                new FileEntry("playwave", "exe", now),
                new FileEntry("q&aview", "exe", now),
                new FileEntry("rbview", "exe", now),
                new FileEntry("refview", "exe", now),
            });
            while (list.Count < 34)
                list.Add(new FileEntry("", "", now));

            // 3-я колонка — твой новый список
            list.AddRange(new List<FileSystemEntry>
            {
                new FileEntry("telemax",  "exe", now),
                new FileEntry("tif2dib",  "exe", now),
                new FileEntry("vector",   "exe", now),
                new FileEntry("wpb2dib",  "exe", now),
                new FileEntry("wpview",   "exe", now),
                new FileEntry("nc",       "ext", now),
                new FileEntry("nc",       "fil", now),
                new FileEntry("ncpscrip", "hdr", now),
                new FileEntry("nc",       "hlp", now),
                new FileEntry("ncff",     "hlp", now),
                new FileEntry("telemax",  "hlp", now),
                new FileEntry("nc",       "ini", now),
                new FileEntry("ncclean",  "ini", now),
                new FileEntry("nc",       "ico", now),
                new FileEntry("telemax",  "ini", now),
            });
            return list;
        }

        public List<FileSystemEntry> GetRightPanelEntries()
        {
            var baseDate = new DateTime(2002, 10, 11, 19, 48, 0);

            var list = new List<FileSystemEntry>
            {
                new DirectoryEntry("..", 0, baseDate),
                new FileEntry("123view", "exe", 128380, baseDate),
                new FileEntry("4372ansi", "set", 255, baseDate),
                new FileEntry("8502ansi", "set", 255, baseDate),
                new FileEntry("8632ansi", "set", 255, baseDate),
                new FileEntry("8652ansi", "set", 255, baseDate),
                new FileEntry("Ajaccgdo\u2593","", 417392, baseDate),
                new FileEntry("ansi2437", "set", 255, baseDate),
                new FileEntry("ansi2850", "set", 255, baseDate),
                new FileEntry("ansi2863", "set", 255, baseDate),
                new FileEntry("ansi2865", "set", 255, baseDate),
                new FileEntry("arcview", "exe", 81738, baseDate),
                new FileEntry("bitmap", "exe", 54805, baseDate),
                new FileEntry("bug", "nss", 16133, baseDate),
                new FileEntry("bungee", "nss", 41914, baseDate),
            };

            // Тоже БЕЗ сортировки
            return list;
        }

    }
}