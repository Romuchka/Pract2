using System;

namespace NortonCommander
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new NortonCommanderUI();
            var fm = new FileManager();

            ui.InitializeConsole();


            // СНАЧАЛА верхняя бирюзовая полоса
            ui.DrawMenu();

            // Потом каркас, панели и низ
            ui.DrawFrameLines();

            var left = fm.GetLeftPanelEntries();
            var right = fm.GetRightPanelEntries();

            ui.DrawLeftPanel(left);
            ui.DrawRightPanel(right);
            ui.DrawHelpBar();

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.CursorVisible = false;
            Console.ReadKey(true);
        }
    }
}
