public abstract class FormScreen : IScreen
{
    public string ScreenName { get; set; }
    protected List<FormField> Fields = new();
    protected int ActiveFieldIndex = 0;

    public abstract void OnFormSubmit();

    public virtual void Start()
    {
        Console.CursorVisible = false;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"==== {ScreenName.ToUpper()} ====\n");

            for (int i = 0; i < Fields.Count; i++)
            {
                var field = Fields[i];
                string displayValue = field.MaskInput ? UserLogic.Mask(field.Value) : field.Value;

                if (i == ActiveFieldIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.ResetColor();
                Console.WriteLine($"{field.Label}: {displayValue}");
            }

            Console.WriteLine("\n[↑][↓] Navigate    [Enter] Edit   [ESC] Cancel   [F]inish");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && ActiveFieldIndex > 0)
                ActiveFieldIndex--;
            else if (key == ConsoleKey.DownArrow && ActiveFieldIndex < Fields.Count - 1)
                ActiveFieldIndex++;
            else if (key == ConsoleKey.Enter)
                EditActiveField();
            else if (key == ConsoleKey.F)
            {
                if (Fields.All(f => f.Validator(f.Value).isValid))
                {
                    OnFormSubmit();
                    return;
                }
                else
                {
                    ShowErrorBox("Please fix validation errors.");
                }
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                return;
            }
        }
    }

    private void EditActiveField()
    {
        var field = Fields[ActiveFieldIndex];
        string input = field.Value;
        int inputTop = Fields.Count + 3;

        Console.SetCursorPosition(0, inputTop);
        Console.WriteLine($"Enter new value for {field.Label}:");
        Console.Write("> ");
        Console.CursorVisible = true;

        while (true)
        {
            Console.SetCursorPosition(2, inputTop + 1);
            Console.Write(new string(' ', Console.WindowWidth - 2));
            Console.SetCursorPosition(2, inputTop + 1);
            Console.Write(field.MaskInput ? UserLogic.Mask(input) : input);

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                input = input[..^1];
            else if (key.Key == ConsoleKey.Enter)
            {
                var (valid, error) = field.Validator(input);
                if (!valid)
                {
                    ShowErrorBox(error);
                }
                else
                {
                    field.Value = input;
                    break;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow && ActiveFieldIndex > 0)
            {
                field.Value = input;
                ActiveFieldIndex--;
                break;
            }
            else if (key.Key == ConsoleKey.DownArrow && ActiveFieldIndex < Fields.Count - 1)
            {
                field.Value = input;
                ActiveFieldIndex++;
                break;
            }
            else if (!char.IsControl(key.KeyChar))
                input += key.KeyChar;
        }

        Console.CursorVisible = false;
    }

    private void ShowErrorBox(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nError: {error}");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
