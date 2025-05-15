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

    public static void ClearConsole()
    {
        Console.CursorVisible = false;

        // Use SetCursorPosition instead of WriteLine to avoid scrolling
        Console.SetCursorPosition(0, 0);

        string blankLine = new string(' ', Console.WindowWidth);
        int height = Console.WindowHeight;

        for (int i = 0; i < height; i++)
        {
            Console.Write(blankLine);

            if (i < height - 1)
                Console.SetCursorPosition(0, i + 1);
        }

        Console.SetCursorPosition(0, 0);
    }

}