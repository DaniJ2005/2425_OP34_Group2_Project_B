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
            General.PrintColoredBoxedTitle($"{ScreenName.ToUpper()}", ConsoleColor.Yellow);
            Console.WriteLine($"\n[↑][↓] Navigate  [Enter] Edit  [ESC] Cancel  {actionLabel}");

            int maxLabelLength = Fields.Max(f => f.Label.Length);
            int maxInputLength = Fields.Max(f => (f.Value ?? "").Length);

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
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.Write("  ");
                    Console.Write($"{labelPadded}: {newValuePadded}");

                    if (isDifferent)
                        Console.Write($"  | {oldValue}");

                    Console.WriteLine();
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < Fields.Count - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
                EditField(selectedIndex);
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
        string originalValue = field.OriginalValue ?? "";
        string input = field.Value ?? "";
        int cursorPos = input.Length;
        bool isInRevertMode = false;

        ConsoleKeyInfo keyInfo;
        bool done = false;

        while (!done)
        {
            bool isDifferent = !string.Equals(input, originalValue);

            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName.ToUpper()}", ConsoleColor.Yellow);
            Console.WriteLine("\n[←][→] Move Cursor  [Backspace] Delete  [Esc] Cancel Edit  [Enter] Confirm");

            int maxLabelLength = Fields.Max(f => f.Label.Length);
            int maxInputLength = Math.Max(input.Length, Fields.Max(f => (f.Value ?? "").Length));

            for (int i = 0; i < Fields.Count; i++)
            {
                var f = Fields[i];
                string displayValue = (i == index) ? input : f.Value ?? "";
                string oldValue = f.OriginalValue ?? "";
                string labelPadded = f.Label.PadRight(maxLabelLength);
                string inputPadded = displayValue.PadRight(maxInputLength);

                if (i == index)
                {
                    Console.Write("> ");
                    Console.Write($"{labelPadded}: ");

                    if (isInRevertMode)
                    {
                        Console.Write(inputPadded);
                        Console.Write($"  | {oldValue}  ");
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[Revert]");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(inputPadded);
                        Console.ResetColor();

                        if (isDifferent)
                        {
                            Console.Write($"  | {oldValue}  ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("[Revert]");
                            Console.ResetColor();
                        }
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.Write("  ");
                    Console.Write($"{labelPadded}: {inputPadded}");

                    if (!string.Equals(displayValue, oldValue))
                        Console.Write($"  | {oldValue}");

                    Console.WriteLine();
                }
            }

            // Position cursor
            int cursorLeft = isInRevertMode
                ? 2 + maxLabelLength + 2 + input.Length + 4 + originalValue.Length + 2
                : 2 + maxLabelLength + 2 + cursorPos;
            int cursorTop = 2 + index;

            cursorLeft = Math.Min(cursorLeft, Console.WindowWidth - 1);
            Console.CursorVisible = !isInRevertMode;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (isInRevertMode)
                    {
                        isInRevertMode = false;
                        cursorPos = input.Length;
                    }
                    else if (cursorPos > 0)
                        cursorPos--;
                    break;

                case ConsoleKey.RightArrow:
                    if (!isInRevertMode && isDifferent)
                        isInRevertMode = true;
                    else if (!isInRevertMode && cursorPos < input.Length)
                        cursorPos++;
                    break;

                case ConsoleKey.Enter:
                    if (isInRevertMode)
                    {
                        input = originalValue;
                        cursorPos = input.Length;
                        isInRevertMode = false;
                    }
                    else
                    {
                        field.Value = input;
                        done = true;
                    }
                    break;

                case ConsoleKey.Backspace:
                    if (!isInRevertMode && cursorPos > 0)
                    {
                        input = input.Remove(cursorPos - 1, 1);
                        cursorPos--;
                    }
                    break;

                case ConsoleKey.Escape:
                    done = true;
                    break;

                default:
                    if (!isInRevertMode && !char.IsControl(keyInfo.KeyChar))
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
