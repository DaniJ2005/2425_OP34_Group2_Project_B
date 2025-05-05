static class GeneralLogic
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

    public static void TextColor(string color)
    {
        if (Enum.TryParse<ConsoleColor>(color, true, out var consoleColor))
        {
            Console.ForegroundColor = consoleColor;
        }
    }
}