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
    
    public static void PrintColoredBoxedTitle(string title, ConsoleColor borderColor, bool fancy = false)
    {
        const int totalWidth = 30;
        int contentWidth = totalWidth - 2;

        if (title.Length > contentWidth)
        {
            title = contentWidth > 3
                ? title.Substring(0, contentWidth - 3) + "..."
                : title.Substring(0, contentWidth);
        }

        // Optional uppercase for fancy
        string displayTitle = fancy ? title.ToUpper() : title;

        int paddingLeft = (contentWidth - displayTitle.Length) / 2;
        int paddingRight = contentWidth - displayTitle.Length - paddingLeft;

        string horizontal = new string('═', contentWidth);
        string spacer = new string(' ', contentWidth);

        // Top border
        Console.ForegroundColor = borderColor;
        Console.WriteLine("╔" + horizontal + "╗");
        Console.ResetColor();

        // Top spacer (fancy only)
        if (fancy)
        {
            Console.ForegroundColor = borderColor;
            Console.Write("║");
            Console.ResetColor();
            Console.Write(spacer);
            Console.ForegroundColor = borderColor;
            Console.WriteLine("║");
            Console.ResetColor();
        }

        // Title line
        Console.ForegroundColor = borderColor;
        Console.Write("║");
        Console.ResetColor();
        Console.Write(new string(' ', paddingLeft));
        Console.Write(displayTitle);
        Console.Write(new string(' ', paddingRight));
        Console.ForegroundColor = borderColor;
        Console.WriteLine("║");
        Console.ResetColor();

        // Bottom spacer (fancy only)
        if (fancy)
        {
            Console.ForegroundColor = borderColor;
            Console.Write("║");
            Console.ResetColor();
            Console.Write(spacer);
            Console.ForegroundColor = borderColor;
            Console.WriteLine("║");
            Console.ResetColor();
        }

        // Bottom border
        Console.ForegroundColor = borderColor;
        Console.WriteLine("╚" + horizontal + "╝");
        Console.ResetColor();
    }

    public static void ClearConsole()
    {
        try
        {
            Console.CursorVisible = false;

            // Ensure the buffer height is not taller than the window height
            if (Console.BufferHeight > Console.WindowHeight)
            {
                Console.BufferHeight = Console.WindowHeight;
            }

            // Set origin
            Console.SetCursorPosition(0, 0);

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            string emptyLine = new string(' ', width);

            for (int i = 0; i < height; i++)
            {
                Console.Write(emptyLine);
            }

            // Reset position after clearing
            Console.SetCursorPosition(0, 0);
        }
        catch (IOException)
        {
            // In case of issues (like redirected output), fall back to Console.Clear
            Console.Clear();
        }
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