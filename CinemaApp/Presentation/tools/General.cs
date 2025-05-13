static class General
{
    public static void PrintColoredString(string str, string color)
    {
        // Try to parse the color string into a ConsoleColor
        if (Enum.TryParse<ConsoleColor>(color, true, out var consoleColor))
        {
            var previousColor = Console.ForegroundColor;

            Console.ForegroundColor = consoleColor;
            Console.Write(str);

            Console.ForegroundColor = previousColor;
        }
        else
        {
            Console.Write(str);
        }
    }

    public static void ClearConsole(int topPosition)
    {
        // Instead of clearing the whole console, just reset to the starting position
        Console.SetCursorPosition(0, topPosition);      

        // Clear the screen area by writing 20 empty lines 
        for (int i = 0; i < 20; i++)
        {
        Console.WriteLine(new string(' ', Console.WindowWidth));
        }

        Console.SetCursorPosition(0, topPosition);   
    }
}