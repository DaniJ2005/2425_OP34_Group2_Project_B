public class UpdateScreen<T> : FormScreen
{
    private readonly Func<T> _mapToUpdatedModel;
    private readonly Func<T, bool> _submitUpdate;
    private const int WrapWidth = 80;

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
            Console.WriteLine();
            Console.WriteLine();

            int maxLabelLength = Fields.Max(f => f.Label.Length);

            for (int i = 0; i < Fields.Count; i++)
            {
                var field = Fields[i];
                string oldValue = field.OriginalValue ?? "";
                string newValue = field.Value ?? "";
                bool isDifferent = !string.Equals(newValue, oldValue);
                var newLines = WrapText(newValue, WrapWidth);
                var oldLines = isDifferent ? WrapText(oldValue, WrapWidth) : new List<string>();
                string labelPadded = field.Label.PadRight(maxLabelLength);

                int lineCount = Math.Max(newLines.Count, oldLines.Count);

                for (int j = 0; j < lineCount; j++)
                {
                    string prefix = i == selectedIndex ? "> " : "  ";
                    Console.Write(prefix);

                    if (j == 0)
                        Console.Write($"{labelPadded}: ");
                    else
                        Console.Write(new string(' ', maxLabelLength + 2));

                    string newLine = j < newLines.Count ? newLines[j] : "";
                    string oldLine = j < oldLines.Count ? oldLines[j] : "";

                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(newLine.PadRight(WrapWidth));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(newLine.PadRight(WrapWidth));
                    }

                    if (j < oldLines.Count)
                    {
                        Console.Write("  | ");
                        Console.Write(oldLine);

                        if (j == oldLines.Count - 1 && isDifferent)
                        {
                            Console.Write("  ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("[Revert]");
                            Console.ResetColor();
                        }
                    }

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
                return;
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
            Console.WriteLine($"Editing '{field.Label}': Type to change the value, or press → to select [Revert] and restore the original.");

            Console.WriteLine();

            int maxLabelLength = Fields.Max(f => f.Label.Length);

            for (int i = 0; i < Fields.Count; i++)
            {
                var f = Fields[i];
                string displayValue = (i == index) ? input : f.Value ?? "";
                string oldValue = f.OriginalValue ?? "";
                string labelPadded = f.Label.PadRight(maxLabelLength);

                var newLines = WrapText(displayValue, WrapWidth);
                var oldLines = (!string.Equals(displayValue, oldValue))
                    ? WrapText(oldValue, WrapWidth)
                    : new List<string>();

                int lineCount = Math.Max(newLines.Count, oldLines.Count);

                for (int j = 0; j < lineCount; j++)
                {
                    string prefix = i == index ? "> " : "  ";
                    Console.Write(prefix);

                    if (j == 0)
                        Console.Write($"{labelPadded}: ");
                    else
                        Console.Write(new string(' ', maxLabelLength + 2));

                    string newLine = j < newLines.Count ? newLines[j] : "";
                    string oldLine = j < oldLines.Count ? oldLines[j] : "";

                    if (i == index)
                    {
                        if (isInRevertMode)
                        {
                            Console.Write(newLine.PadRight(WrapWidth));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(newLine.PadRight(WrapWidth));
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Write(newLine.PadRight(WrapWidth));
                    }

                    if (j < oldLines.Count)
                    {
                        Console.Write("  | ");
                        Console.Write(oldLine);

                        if (j == oldLines.Count - 1 && isDifferent && i == index)
                        {
                            Console.Write("  ");

                            if (isInRevertMode)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("[Revert]");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("[Revert]");
                                Console.ResetColor();
                            }
                        }
                    }

                    Console.WriteLine();
                }
            }

            int cursorTop = 2 + Fields.Take(index).Sum(f => Math.Max(WrapText(f.Value ?? "", WrapWidth).Count,
                                                                     WrapText(f.OriginalValue ?? "", WrapWidth).Count));
            int cursorLeft = 2 + maxLabelLength + 2 + cursorPos;
            cursorLeft = Math.Min(cursorLeft, Console.WindowWidth - 1);
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

    private List<string> WrapText(string text, int maxWidth)
    {
        var lines = new List<string>();
        for (int i = 0; i < text.Length; i += maxWidth)
        {
            lines.Add(text.Substring(i, Math.Min(maxWidth, text.Length - i)));
        }
        return lines;
    }
}
