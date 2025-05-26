public abstract class FormScreen : IScreen
{
    public string ScreenName { get; set; }
    protected List<FormField> Fields = new();
    protected int ActiveFieldIndex = 0;

    public abstract void OnFormSubmit();

    public virtual void Start()
    public virtual void Start()
    {
        General.ClearConsole();

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
                continue; // retry this field
            }

            ActiveFieldIndex++;
        }

        OnFormSubmit();
    }

    string ReadInput(FormField field)
    {
        string input = field.Value ?? "";
        ConsoleKeyInfo key;
        int cursorTop;

        while (true)
        {
            General.ClearConsole();
            Console.WriteLine($"==== {ScreenName.ToUpper()} ====\n");

            for (int i = 0; i < Fields.Count; i++)
            {
                bool? isValid = f == field ? null : f.Validator(f.Value).isValid;
                ShowField(f.Label, f.MaskInput ? UserLogic.Mask(f.Value) : f.Value, f.IsActive, isValid);
            }

            Console.Write($"\n> {field.Label}: ");
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            cursorTop = top;

            Console.WriteLine(); // reserve error space

            while (true)
            {
                Console.SetCursorPosition(left, cursorTop);
                Console.Write(new string(' ', Console.WindowWidth - left));
                Console.SetCursorPosition(left, cursorTop);
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

    void ShowField(string label, string value, bool isActive, bool? isValid)
    {
        string prefix = isActive ? "> " : "  ";

        Console.ForegroundColor = isValid == true ? ConsoleColor.Green :
                                  isValid == false ? ConsoleColor.Red :
                                  ConsoleColor.White;

        if (isActive)
            Console.BackgroundColor = ConsoleColor.DarkGray;

        Console.WriteLine($"{prefix}{label}: {value}");
        Console.ResetColor();
    }

    private void ShowErrorBox(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nError: {error}");
        Console.ResetColor();
        Console.WriteLine("Press any key to retry...");
        Console.ReadKey(true);
    }
}
