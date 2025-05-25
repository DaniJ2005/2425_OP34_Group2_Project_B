public class UpdateScreen<T> : FormScreen
{
    private readonly Func<T> _mapToUpdatedModel;
    private readonly Func<T, bool> _submitUpdate;

    public UpdateScreen(string screenName, List<FormField> fields,
        Func<T> mapToUpdatedModel, Func<T, bool> submitUpdate)
    {
        ScreenName = screenName;
        Fields = fields;
        _mapToUpdatedModel = mapToUpdatedModel;
        _submitUpdate = submitUpdate;
    }

    public override void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        string actionLabel = ScreenName.StartsWith("Update", StringComparison.OrdinalIgnoreCase)
            ? $"[U]pdate {ScreenName.Substring(7)}"
            : "[S]ubmit";

        do
        {
            General.ClearConsole();
            Console.WriteLine($"=== {ScreenName.ToUpper()} ===\n");

            int maxLabelLength = Fields.Max(f => f.Label.Length);
            int maxInputLength = 0;
            for (int i = 0; i < Fields.Count; i++)
            {
                var val = Fields[i].Value ?? "";
                if (val.Length > maxInputLength) maxInputLength = val.Length;
            }

            for (int i = 0; i < Fields.Count; i++)
            {
                var field = Fields[i];
                string oldValue = field.OriginalValue ?? "";
                string newValue = field.Value ?? "";

                bool isDifferent = !string.Equals(newValue, oldValue);

                string labelPadded = field.Label.PadRight(maxLabelLength);
                string newValuePadded = newValue.PadRight(maxInputLength);

                if (i == selectedIndex)
                {
                    // Selected line, highlight with "> " and yellow foreground for newValue
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("> ");
                    Console.ResetColor();

                    Console.Write($"{labelPadded}: ");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(newValuePadded);
                    Console.ResetColor();

                    if (isDifferent)
                    {
                        Console.Write($"  | {oldValue}  ");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[R]evert");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                else
                {
                    // Normal lines
                    Console.Write("  ");
                    Console.Write($"{labelPadded}: {newValuePadded}");

                    if (isDifferent)
                        Console.Write($"  | {oldValue}");

                    Console.WriteLine();
                }
            }

            Console.WriteLine($"\n[↑][↓] Navigate  [Enter] Edit  [ESC] Cancel  {actionLabel}");
            Console.WriteLine("Press [R] to revert current field if changed.");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < Fields.Count - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
                EditField(selectedIndex);
            else if (key == ConsoleKey.R)
            {
                var f = Fields[selectedIndex];
                if (!string.Equals(f.Value, f.OriginalValue))
                    f.Value = f.OriginalValue;
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                return;
            }
            else if (key == ConsoleKey.U || key == ConsoleKey.S)
            {
                if (ValidateAllFields())
                {
                    OnFormSubmit();
                    break;
                }
            }
        } while (true);
    }

    private void EditField(int index)
    {
        var field = Fields[index];
        string input = field.Value ?? "";
        int cursorPos = input.Length;

        ConsoleKeyInfo keyInfo;
        bool done = false;

        while (!done)
        {
            General.ClearConsole();
            Console.WriteLine($"=== {ScreenName.ToUpper()} ===\n");

            int maxLabelLength = Fields.Max(f => f.Label.Length);
            int maxInputLength = 0;
            for (int i = 0; i < Fields.Count; i++)
            {
                var f = Fields[i];
                string val = (i == index) ? input : f.Value ?? "";
                if (val.Length > maxInputLength) maxInputLength = val.Length;
            }

            for (int i = 0; i < Fields.Count; i++)
            {
                var f = Fields[i];
                string oldValue = f.OriginalValue ?? "";
                bool isEditingField = (i == index);

                string displayValue = isEditingField ? input : f.Value ?? "";
                bool isDifferent = !string.Equals(displayValue, oldValue);

                string labelPadded = f.Label.PadRight(maxLabelLength);
                string inputPadded = displayValue.PadRight(maxInputLength);

                if (isEditingField)
                {
                    Console.Write("> ");
                    Console.Write($"{labelPadded}: ");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(inputPadded);
                    Console.ResetColor();

                    if (isDifferent)
                        Console.Write($"  | {oldValue}");

                    Console.WriteLine();
                }
                else
                {
                    Console.Write("  ");
                    Console.Write($"{labelPadded}: {inputPadded}");

                    if (isDifferent)
                        Console.Write($"  | {oldValue}");

                    Console.WriteLine();
                }
            }

            Console.WriteLine("\n[←][→] Move Cursor  [Backspace] Delete  [Esc] Cancel Edit  [Enter] Confirm  [R] Revert");

            int cursorLeft = 2 + maxLabelLength + 2 + cursorPos; // "> " + label + ": "
            int cursorTop = 2 + index;

            if (cursorLeft >= Console.WindowWidth)
                cursorLeft = Console.WindowWidth - 1;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.CursorVisible = true;

            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (cursorPos > 0) cursorPos--;
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorPos < input.Length) cursorPos++;
                    break;
                case ConsoleKey.Backspace:
                    if (cursorPos > 0)
                    {
                        input = input.Remove(cursorPos - 1, 1);
                        cursorPos--;
                    }
                    break;
                case ConsoleKey.Enter:
                    field.Value = input;
                    done = true;
                    break;
                case ConsoleKey.Escape:
                    done = true;
                    break;
                case ConsoleKey.R:
                    input = field.OriginalValue ?? "";
                    cursorPos = input.Length;
                    break;
                default:
                    if (!char.IsControl(keyInfo.KeyChar))
                    {
                        input = input.Insert(cursorPos, keyInfo.KeyChar.ToString());
                        cursorPos++;
                    }
                    break;
            }
        }

        Console.CursorVisible = false;
    }

    public override void OnFormSubmit()
    {
        var updatedModel = _mapToUpdatedModel();
        bool success = _submitUpdate(updatedModel);
        Console.WriteLine(success ? "\nUpdated successfully!" : "\nUpdate failed.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        MenuLogic.NavigateToPrevious();
    }

    private bool ValidateAllFields()
    {
        foreach (var field in Fields)
        {
            var (valid, error) = field.Validator?.Invoke(field.Value) ?? (true, "");
            if (!valid)
            {
                Console.WriteLine($"\nInvalid input for '{field.Label}': {error}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }
        }
        return true;
    }
}
