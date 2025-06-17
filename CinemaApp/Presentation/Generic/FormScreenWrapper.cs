public class FormScreenWrapper
{
    string screenName;
    List<FormField> fields;

    public FormScreenWrapper(string screenName = "", List<FormField> fields = null)
    {
        this.screenName = screenName;
        this.fields = fields;
    }

    public string ReadFieldInput(FormField field)
    {
        string input = "";
        ConsoleKeyInfo key;
        int cursorTop = Console.CursorTop;

        while (true)
        {
            General.ClearConsole();
            Console.WriteLine($"==== {screenName} ====\n");

            foreach (var f in fields)
            {
                bool? isValid = f == field ? null : f.Validator(f.Value).isValid;
                ShowField(f.Label, f.MaskInput ? UserLogic.Mask(f.Value) : f.Value, f.IsActive, isValid);
            }

            Console.Write($"\n> {field.Label}: ");
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.WriteLine(); // reserve error space

            while (true)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(new string(' ', Console.WindowWidth - left));
                Console.SetCursorPosition(left, top);
                Console.Write(field.MaskInput ? UserLogic.Mask(input) : input);

                Console.CursorVisible = true;
                key = Console.ReadKey(true);
                Console.CursorVisible = false;

                if (key.Key == ConsoleKey.Escape)
                {
                    MenuLogic.NavigateToPrevious();
                    return null;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input[..^1];
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return input;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                }
            }
        }
    }

    public void ShowErrorBox(string error)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        int width = error.Length + 4;
        Console.WriteLine("  ╭" + new string('─', width) + "╮");
        Console.WriteLine($"  │  {error}  │");
        Console.WriteLine("  ╰" + new string('─', width) + "╯");
        Console.ResetColor();
        Console.ReadKey();
    }

    void ShowField(string label, string value, bool isActive, bool? isValid)
    {
        var prefix = isActive ? "> " : "  ";

        Console.ForegroundColor = isValid == true ? ConsoleColor.Green :
                                  isValid == false ? ConsoleColor.Red :
                                  ConsoleColor.White;

        if (isActive) Console.BackgroundColor = ConsoleColor.DarkGray;

        Console.WriteLine($"{prefix}{label}: {value}");
        Console.ResetColor();
    }
}
