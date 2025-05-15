public abstract class FormScreen : IScreen
{
    public string ScreenName { get; set; }

    protected List<FormField> Fields = new();
    protected int ActiveFieldIndex = 0;

    public abstract void OnFormSubmit();

    public void Start()
    {
        Console.Clear();
        Console.CursorVisible = true;

        while (ActiveFieldIndex < Fields.Count)
        {
            var field = Fields[ActiveFieldIndex];
            field.IsActive = true;

            string input = ReadInput(field);

            if (input == null)
                return; // user canceled

            field.Value = input;
            field.IsActive = false;

            var (isValid, errorMessage) = field.Validator(input);
            if (!isValid)
            {
                ShowErrorBox(errorMessage);
                continue;
            }

            ActiveFieldIndex++;
        }

        OnFormSubmit();
    }

    string ReadInput(FormField field)
    {
        string input = "";
        ConsoleKeyInfo key;
        int cursorTop = Console.CursorTop;

        while (true)
        {
            General.ClearConsole(cursorTop);
            Console.WriteLine($"==== {ScreenName} ====\n");

            foreach (var f in Fields)
            {
                bool? isValid = f == field ? null : f.Validator(f.Value).isValid;
                ShowField(f.Label, f.MaskInput ? UserLogic.Mask(f.Value) : f.Value, f.IsActive, isValid);
            }

            Console.Write($"\n> {field.Label}: ");
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.WriteLine(); // reserve space for error

            while (true)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(new string(' ', Console.WindowWidth - left));
                Console.SetCursorPosition(left, top);
                Console.Write(field.MaskInput ? UserLogic.Mask(input) : input);

                key = Console.ReadKey(true);

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

    void ShowErrorBox(string error)
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
}
