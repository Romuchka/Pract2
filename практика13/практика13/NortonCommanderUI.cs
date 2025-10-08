using System;
using System.Collections.Generic;
using System.Text;

namespace NortonCommander
{
    public class NortonCommanderUI
    {
        private const int W = 96;
        private const int H = 24;

        // Центр: две стойки подряд, без зазора
        private const int SepX = 45;     // левая центральная стойка
        private const int CX1 = SepX;                // ║
        private const int CX2 = SepX + 1;            // ║

        // Горизонтали панелей
        private const int TopLineY = 1;            // верхняя двойная линия с "C:\\NC"
        private const int PanelTop = TopLineY + 1; // строка заголовков
        private const int PanelBottom = H - 5;        // последняя строка данных

        // Рабочие зоны панелей (учитываем стойки и борта)
        private const int LeftStartX = 1;            // после левого борта
        private const int LeftEndX = CX1 - 1;      // перед левой стойкой
        private const int RightStartX = CX2 + 1;      // после правой стойки
        private const int RightEndX = W - 2;        // перед правым бортом

        // Псевдографика (двойные линии)
        private const char H2 = '\u2550'; // ═
        private const char V2 = '\u2551'; // ║
        private const char UL = '\u2554'; // ╔
        private const char UR = '\u2557'; // ╗
        private const char LL = '\u255A'; // ╚
        private const char LR = '\u255D'; // ╝
        private const char CN = '\u2502'; // |
        private const char TJ = '\u2564'; // ╤
        private const char H1 = '\u2500'; // ─
        private const char B1 = '\u2534'; // ┴ 
        private const char LVD = '\u255F'; // ╟  
        private const char RVD = '\u2562'; // ╢  


        public void InitializeConsole()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            try { Console.SetWindowSize(Math.Max(1, W - 2), Math.Max(1, H - 1)); Console.SetBufferSize(W, H); } catch { }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
        }

        public void DrawFrameLines()
        {
            // Периметр
            WriteAt(0, TopLineY, new string(H2, W));
            WriteAt(0, H - 4, new string(H2, W));
            WriteAt(0, TopLineY, UL.ToString()); // ╔
            WriteAt(W - 1, TopLineY, UR.ToString()); // ╗
            WriteAt(0, H - 4, LL.ToString()); // ╚
            WriteAt(W - 1, H - 4, LR.ToString()); // ╝
            WriteAt(CX1, H - 4, LR.ToString()); // ╝ под левой стойкой
            WriteAt(CX2, H - 4, LL.ToString()); // ╚ под правой стойкой


            for (int y = TopLineY + 1; y <= H - 5; y++)
            {
                WriteAt(0, y, V2.ToString());
                WriteAt(W - 1, y, V2.ToString());
            }

            // Центральные углы: РОВНО в CX1/CX2 — соседей НЕ трогаем
            WriteAt(CX1, TopLineY, UR.ToString()); // ╗
            WriteAt(CX2, TopLineY, UL.ToString()); // ╔


            // Заголовки в верхней линии
            WriteTitleClamped(LeftStartX, LeftEndX, "C:\\NC", TopLineY);
            // Убираем по одной ═ по бокам от "C:\NC" в левой панели
            {
                string title = "C:\\NC";
                int leftWidth = LeftEndX - LeftStartX + 1;
                int start = LeftStartX + Math.Max(0, (leftWidth - title.Length) / 2);

                // слева от текста
                int lx = start - 1;
                if (lx >= LeftStartX && lx < CX1) WriteAt(lx, TopLineY, " ");

                // справа от текста
                int rx = start + title.Length;
                if (rx <= LeftEndX && rx < CX1) WriteAt(rx, TopLineY, " ");
            }

            // Убираем по одной ═ по бокам от "C:\NC" в правой панели
            {
                string title = "C:\\NC";
                int rightWidth = RightEndX - RightStartX + 1;
                int start = RightStartX + Math.Max(0, (rightWidth - title.Length) / 2);

                // по одному пробелу-сайдпаду слева и справа, тоже на Cyan
                int padL = start - 1;
                int padR = start + title.Length;

                // левая «рамка» плашки 
                if (padL >= RightStartX && padL > CX2 && padL < W - 1)
                    WriteAt(padL, TopLineY, " ", ConsoleColor.DarkBlue, ConsoleColor.Cyan);

                // сам заголовок
                WriteAt(start, TopLineY, title, ConsoleColor.DarkBlue, ConsoleColor.Cyan);

                // правая «рамка» плашки
                if (padR <= RightEndX && padR > CX2 && padR < W - 1)
                    WriteAt(padR, TopLineY, " ", ConsoleColor.DarkBlue, ConsoleColor.Cyan);
            }


            // Стойки ║║ 
            for (int y = TopLineY + 1; y <= H - 5; y++)
            {
                WriteAt(CX1, y, V2.ToString());
                WriteAt(CX2, y, V2.ToString());
            }
        }

        private void WriteTitleClamped(int x1, int x2, string text, int y)
        {
            int width = Math.Max(0, x2 - x1 + 1);
            if (width <= 0 || string.IsNullOrEmpty(text)) return;
            if (text.Length > width) text = text.Substring(0, width);

            int start = x1 + Math.Max(0, (width - text.Length) / 2);

            // не допускаем перекрытия центральных углов
            if ((start <= CX1 && start + text.Length - 1 >= CX1) ||
                (start <= CX2 && start + text.Length - 1 >= CX2))
                return;

            WriteAt(start, y, text, ConsoleColor.Cyan);
        }

        // ----------------------------- Левая панель ----------------------------
        public void DrawLeftPanel(List<FileSystemEntry> entries)
        {
            int nameW = 10, extW = 3;
            int colW = nameW + 1 + extW; // 14
            int gap = 1, columns = 3;

            // Заголовки колонок — жёлтым и по центру
            for (int c = 0; c < columns; c++)
            {
                // базовый X: для 0-й колонки остаётся LeftStartX,
                // для остальных — сразу справа от их разделителя │
                int baseX = (c == 0)
                    ? LeftStartX
                    : (LeftStartX + c * (colW + gap) - gap + 1);

                if (baseX > LeftEndX) break;

                int headX = baseX + (nameW - "Имя".Length) / 2;
                WriteAt(headX, PanelTop, "Имя", ConsoleColor.Yellow); // без ведущего пробела
            }
            // "C:↓" оставляем как есть
            WriteAt(LeftStartX, PanelTop, "C:\u2193", ConsoleColor.Yellow);


            // Внутренние разделители колонок: вверх ╤ на двойную ═, далее │, внизу ╧
            // Внутренние разделители колонок: сверху ╤ на двойную ═, далее │, БЕЗ нижней крышки
            for (int c = 1; c < columns; c++)
            {
                int x = LeftStartX + c * (colW + gap) - gap;
                if (x >= LeftEndX) break;

                WriteAt(x, TopLineY, TJ.ToString());            
                DrawVertical(x, PanelTop, PanelBottom - 2, CN); 

            }

            int rowsPerCol = PanelBottom - PanelTop; // строки под данные в каждой колонке
            int maxItems = columns * rowsPerCol;


            for (int i = 0; i < Math.Min(entries.Count, maxItems); i++)
            {
                int c = i / rowsPerCol;
                int r = i % rowsPerCol;

                int baseX = (c == 0)
                    ? LeftStartX
                    : (LeftStartX + c * (colW + gap) - gap + 1);

                int y = PanelTop + 1 + r;

                if (baseX + colW - 1 > LeftEndX) break;

                string nameOnly, extOnly;

                if (entries[i] is FileEntry fe)
                {
                    nameOnly = fe.Name ?? "";
                    extOnly = fe.Extension ?? "";
                }
                else
                {
                    string full = entries[i].Name ?? "";
                    int dot = full.LastIndexOf('.');
                    if (dot > 0 && dot < full.Length - 1)
                    {
                        nameOnly = full.Substring(0, dot);
                        extOnly = full.Substring(dot + 1);
                    }
                    else
                    {
                        nameOnly = full;
                        extOnly = "";
                    }
                }

                if (nameOnly.Length > nameW) nameOnly = nameOnly.Substring(0, nameW);
                if (extOnly.Length > extW) extOnly = extOnly.Substring(0, extW);

                var color = ConsoleColor.Cyan;
                WriteAt(baseX, y, PadRight(nameOnly, nameW), color);
                WriteAt(baseX + nameW + 1, y, PadRight(extOnly, extW), color);
            }
            // Нижняя «полка» левой панели: сплошная ─ без внутренних │
            WriteAt(LeftStartX, PanelBottom -1, new string(H1, LeftEndX - LeftStartX + 1));
            // Стыки «полки» с крайними двойными стойками
            WriteAt(0, PanelBottom - 1, LVD.ToString()); // слева: ╟ на колонке V2
            WriteAt(CX1, PanelBottom - 1, RVD.ToString()); // справа: ╢ на двойной стойке

            // Две точки на левом краю полки: ". ."
            {
                const string dots = "..";
                int yShelf = PanelBottom ;
                int xDots = LeftStartX ; // не трогаем ╟ на x=0

                // страховка, чтобы не вылезти за границу панели
                if (xDots + dots.Length - 1 <= LeftEndX)
                    WriteAt(xDots, yShelf, dots);
            }

            // Узлы ┴ на местах внутренних разделителей поверх полки ─
            for (int c = 1; c < columns; c++)
            {
                int x = LeftStartX + c * (colW + gap) - gap;
                if (x >= LeftEndX) break;
                WriteAt(x, PanelBottom - 1, B1.ToString()); // ┴
            }

            // Статусная строка в левой панели (как справа)
            {
                string status = "▸КАТАЛОГ◂ 11.10.02  19:48";
                int leftWidth = LeftEndX - LeftStartX + 1;

                // на сколько сдвигаем вправо (подбери по вкусу: 1, 2, 3…)
                const int LeftStatusShift = 10;

                int sx = LeftStartX + Math.Max(0, (leftWidth - status.Length) / 2) + LeftStatusShift;

                // не даём строке уехать за правую границу панели
                sx = Math.Min(sx, LeftEndX - status.Length + 1);

                WriteAt(sx, H - 5, status);

            }

        }

        // ---------------------------- Правая панель ----------------------------
        public void DrawRightPanel(List<FileSystemEntry> entries)
        {
            int startX = RightStartX;
            int y = PanelTop;

            // первая область шириной 15 теперь = имя (11) + пробел (1) + расширение (3)
            int nameTotalW = 15;
            int nameW = 11;   // ширина под имя
            int extW = 3;    // ширина под расширение
            int sizeW = 11, dateW = 8, timeW = 5;

            // разделители (как были раньше)
            int x1 = startX + nameTotalW;         // Имя(+расширение)
            int x2 = x1 + 1 + sizeW;
            int x3 = x2 + 1 + dateW;

            // Заголовки — жёлтые и по центру
            WriteCentered(startX, nameTotalW, PanelTop, "Имя", ConsoleColor.Yellow);
            // C:↓ у левого края панели
            WriteAt(RightStartX, PanelTop, "C:\u2193", ConsoleColor.Yellow);

            // Вертикальные разделители │ (подняты к верхней ═ через ╤)
            void ColSep(int x)
            {
                WriteAt(x, TopLineY, TJ.ToString());                 // ╤ поверх 
                DrawVertical(x, PanelTop, PanelBottom - 2, CN);      // │ до полки
            }
            ColSep(x1);
            WriteCentered(x1 + 1, sizeW, PanelTop, "Размер", ConsoleColor.Yellow);

            ColSep(x2);
            WriteCentered(x2 + 1, dateW, PanelTop, "Дата", ConsoleColor.Yellow);

            ColSep(x3);
            WriteCentered(x3 + 1, timeW, PanelTop, "Время", ConsoleColor.Yellow);

            y++;

            int rowIndex = 0;
            foreach (var e in entries)
            {
                if (y > PanelBottom - 2) break;
                rowIndex++;

                // имя и расширение — отдельно
                string nameOnly = e.Name ?? "";
                string extOnly = (e is FileEntry fe ? (fe.Extension ?? "") : "");

                nameOnly = nameOnly.Length > nameW ? nameOnly.Substring(0, nameW) : nameOnly.PadRight(nameW);
                extOnly = extOnly.Length > extW ? extOnly.Substring(0, extW) : extOnly.PadRight(extW);

                string sizeStr = e.IsDirectory ? "▸КАТАЛОГ◂".PadLeft(sizeW)
                                               : (e is FileEntry f ? f.Size.ToString().PadLeft(sizeW) : "".PadLeft(sizeW));
                string date =
                    (rowIndex == 1) ? "11.10.02".PadRight(dateW) :
                    (rowIndex == 7) ? "12.10.02".PadRight(dateW) :
                         "22.05.95".PadRight(dateW);
                string time = (e.IsDirectory && e.Name == "..")
                     ? "19:48".PadRight(timeW)   // только первая строка (подсветка)
                     : "5:00".PadRight(timeW);   // все остальные строки

                // >>> Подсветка строки с ".."
                bool highlight = e.IsDirectory && e.Name == "..";

                if (highlight)
                {
                    // 1) Сплошная заливка всей строки правой панели под шапкой
                    int bandWidth = RightEndX - RightStartX + 1;
                    WriteAt(RightStartX, y, new string(' ', bandWidth), ConsoleColor.DarkBlue, ConsoleColor.Cyan);

                    WriteAt(x1, y, CN.ToString(), ConsoleColor.DarkBlue, ConsoleColor.Cyan);
                    WriteAt(x2, y, CN.ToString(), ConsoleColor.DarkBlue, ConsoleColor.Cyan);
                    WriteAt(x3, y, CN.ToString(), ConsoleColor.DarkBlue, ConsoleColor.Cyan);
                }

                // Цвета текста для текущей строки
                ConsoleColor fg = highlight ? ConsoleColor.DarkBlue : ConsoleColor.Cyan;
                ConsoleColor? bg = highlight ? ConsoleColor.Cyan : (ConsoleColor?)null;

                // имя + расширение
                WriteAt(startX, y, nameOnly, fg, bg);
                WriteAt(startX + nameW + 1, y, extOnly, fg, bg);

                // остальные колонки
                WriteAt(x1 + 1, y, sizeStr, fg, bg);
                WriteAt(x2 + 1, y, date, fg, bg);
                WriteAt(x3 + 1, y, time, fg, bg);


                y++;
            }

            // Нижняя «полка» правой панели
            WriteAt(RightStartX, PanelBottom - 1, new string(H1, RightEndX - RightStartX + 1));
            WriteAt(CX2, PanelBottom - 1, LVD.ToString());   // слева ╟
            WriteAt(W - 1, PanelBottom - 1, RVD.ToString()); // справа ╟

            // точки слева под полкой
            {
                const string dots = "..";
                int yShelf = PanelBottom;
                int xDots = RightStartX;
                if (xDots + dots.Length - 1 <= RightEndX)
                    WriteAt(xDots, yShelf, dots);
            }

            // узлы ┴ под разделителями
            WriteAt(x1, PanelBottom - 1, B1.ToString());
            WriteAt(x2, PanelBottom - 1, B1.ToString());
            WriteAt(x3, PanelBottom - 1, B1.ToString());

            // статус справа
            WriteAt(CX1 + 25, H - 5, "▸КАТАЛОГ◂ 11.10.02  19:48");
        }


        // ----------------------------- Низ интерфейса --------------------------
        public void DrawHelpBar()
        {
            FillLine(H - 3, ConsoleColor.Black, ConsoleColor.Black);
            WriteAt(LeftStartX, H - 3, "C:\\NC>", ConsoleColor.White, ConsoleColor.Black);

            string[] k = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[] v = { "Помощь", "Вызов", "Чтение", "Правка", "Копия", "НовИмя", "НовКат", "Удал-е", "Меню", "Выход" };
            const int leftMargin = 1; // ширина чёрной полоски слева
            const int rightMargin = 1; // ширина чёрной полоски справа

            // рисуем левую чёрную полоску
            WriteAt(0, H - 2, new string(' ', leftMargin), ConsoleColor.Black, ConsoleColor.Black);

            int pos = 1;
            WriteAt(pos, H - 2, new string(' ', Math.Max(0, W - rightMargin - pos)), ConsoleColor.Black, ConsoleColor.Black);
            for (int i = 0; i < k.Length; i++)
            {
                WriteAt(pos, H - 2, k[i], ConsoleColor.White, ConsoleColor.Black);
                pos += k[i].Length;
                WriteAt(pos, H - 2, v[i] + "  ", ConsoleColor.Black, ConsoleColor.DarkCyan);
                pos += v[i].Length + 2;
                if (pos >= W - 2) break;
            }
        }


        // -------------------------- Вспомогательные ----------------------------
        private void WriteCentered(int x, int w, int y, string text, ConsoleColor color)
        {
            int start = x + Math.Max(0, (w - text.Length) / 2);
            WriteAt(start, y, PadCut(text, w), color);
        }

        private static string PadRight(string s, int w)
        {
            if (string.IsNullOrEmpty(s)) s = "";
            if (s.Length >= w) return s.Substring(0, w);
            return s.PadRight(w);
        }

        private static string PadCut(string s, int w)
        {
            if (string.IsNullOrEmpty(s)) s = "";
            if (s.Length >= w) return s.Substring(0, w);
            return s.PadRight(w);
        }

        private void DrawVertical(int x, int fromY, int toY, char ch)
        {
            for (int y = fromY; y <= toY; y++)
                WriteAt(x, y, ch.ToString());
        }

        private void FillLine(int y, ConsoleColor fg, ConsoleColor bg)
        {
            var pf = Console.ForegroundColor; var pb = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = fg;
                Console.BackgroundColor = bg;
                Console.SetCursorPosition(0, y);
                Console.Write(new string(' ', W));
            }
            finally { Console.ForegroundColor = pf; Console.BackgroundColor = pb; }
        }

        private void WriteAt(int x, int y, string text, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            if (x < 0 || y < 0 || x >= W || y >= H) return;

            var pf = Console.ForegroundColor; var pb = Console.BackgroundColor;
            try
            {
                if (fg.HasValue) Console.ForegroundColor = fg.Value;
                if (bg.HasValue) Console.BackgroundColor = bg.Value;
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
            finally
            {
                Console.ForegroundColor = pf; Console.BackgroundColor = pb;
            }
        }
        public void DrawMenu()
        {
            // Бирюзовая полоса в самом верху
            FillLine(0, ConsoleColor.Black, ConsoleColor.DarkCyan);

            // Слова меню
            string[] words = { "Левая", "Файл", "Диск", "Команды", "Правая" };
            int x = 4;

            foreach (var w in words)
            {
                // первая буква — жёлтая
                WriteAt(x, 0, w.Substring(0, 1), ConsoleColor.Yellow, ConsoleColor.DarkCyan);
                // остальные — чёрные
                if (w.Length > 1)
                    WriteAt(x + 1, 0, w.Substring(1), ConsoleColor.Black, ConsoleColor.DarkCyan);

                x += w.Length + 5; // пробелов между словами
                if (x >= W - 4) break; // страховка от выхода за край
            }

            const string topRight = "8 30";
            WriteAt(W - topRight.Length, 0, topRight, ConsoleColor.DarkBlue, ConsoleColor.Cyan);
        }

    }
}
