using System;
using System.IO;

class SymbolCheckScreen : IScreen
{
    public string ScreenName { get; set; }

    public SymbolCheckScreen() => ScreenName = "Symbol Check";

    public void Start()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;

        string testSymbols = "← ↑ → ↓ ▧ ▣ ■ □";

        General.ClearConsole();

        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║           QUICK DISPLAY CHECK           ║");
        Console.WriteLine("╚═════════════════════════════════════════╝\n");

        Console.WriteLine("Before we start, let's quickly check if your screen shows our symbols correctly.");
        Console.WriteLine("These symbols help show seats and directions in the app.\n");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("   Symbols:  " + testSymbols);
        Console.ResetColor();

        Console.WriteLine("\nDo the symbols above look like arrows and boxes?");
        Console.WriteLine("Or are they just question marks or weird characters?\n");

        Console.WriteLine("Press:");
        Console.WriteLine("  [Y] if they look normal (arrows and squares)");
        Console.WriteLine("  [N] if they look broken (question marks or symbols missing)");

        ConsoleKey key = Console.ReadKey(true).Key;
        Console.WriteLine();

        if (key == ConsoleKey.Y)
        {
            SessionDataLogic.MarkSymbolCheckPassed();
        }
        else
        {                        
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nNo problem! This might happen on some computers or terminals.");
            Console.WriteLine("Some symbols might not look right during the app.");
            Console.WriteLine("Tip: Try using a better font (like Consolas or Cascadia Code)");
            Console.WriteLine("or running this app in Windows Terminal for the best look.");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
