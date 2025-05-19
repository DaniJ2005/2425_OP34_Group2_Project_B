using System.Reflection;
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
        // Hide the cursor for a cleaner look while redrawing
        Console.CursorVisible = false;

        // Cache the blank line and console dimensions
        string blankLine = new string(' ', Console.WindowWidth);
        int height = Console.WindowHeight;

        // Minimize flicker by locking cursor at (0, 0) and overwriting line-by-line
        Console.SetCursorPosition(0, 0);
        
        for (int i = 0; i < height; i++)
        {
            Console.Write(blankLine);
        }

        // Reset to top-left corner
        Console.SetCursorPosition(0, 0);
    }


    public static void PrintProperties(object obj)
    {
        if (obj == null)
        {
            Console.WriteLine("Object is null.");
            return;
        }

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        Console.WriteLine($"Properties of {type.Name}:");

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj, null);
            Console.Write($"{prop.Name}: {value} | ");
        }
    }
}