// Enable UTF-8 output
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Test if the terminal supports common Unicode symbols
string testSymbols = "← ↑ → ↓ ▧ ▣ ■ □";
Console.WriteLine("Checking Unicode symbol support...");

Console.WriteLine($"Test: {testSymbols}");
Console.Write("Do the symbols above look correct? [Y/N]: ");
var key = Console.ReadKey().Key;
Console.WriteLine();

if (key != ConsoleKey.Y)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nWarning: Your terminal may not fully support Unicode symbols.");
    Console.WriteLine("Some graphics or seat markers may not appear correctly.");
    Console.WriteLine("Try using a Unicode-friendly font (e.g., Consolas, Cascadia Code) or a better terminal (e.g., Windows Terminal, VS Code).");
    Console.ResetColor();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
}


//Dev only
Db.DeleteTables();

// Initialize db tables
Db.InitTables();

// Fill db with some data
Db.PopulateTables();

// Start application
MenuLogic.NavigateTo(new HomeScreen());