public class LoginScreen : IScreen
{
    public string ScreenName { get; set; }

    public LoginScreen() => ScreenName = "Login";

    string email = "";
    string password = "";
    User foundUser = null;

    public void Start()
    {
        Console.Clear();
        Console.CursorVisible = true;

        string errorEmail = "";
        string errorPassword = "";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Login Form ==== (Press Esc to cancel)\n");

            bool? emailValid = null;
            if (!string.IsNullOrWhiteSpace(email))
            {
                emailValid = foundUser != null ? true : errorEmail != "" ? false : null;
            }

            ShowField("Email", email, string.IsNullOrEmpty(email), emailValid);
            ShowField("Password", UserLogic.Mask(password), false, null);

            Console.Write($"\n> Email: ");
            email = ReadInput(false, ref errorEmail);
            if (email == null) return; // user cancelled

            foundUser = UserLogic.CheckEmail(email);
            if (foundUser == null)
            {
                errorEmail = "Email not found.";
                continue;
            }

            // Ask for Password
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Login Form ====\n");

                ShowField("Email", email, false, true);

                bool? passwordValid = !string.IsNullOrEmpty(password) ? errorPassword == "" : null;
                ShowField("Password", UserLogic.Mask(password), true, passwordValid);

                Console.Write($"\n> Password: ");
                password = ReadInput(true, ref errorPassword);
                if (password == null) return;

                if (UserLogic.VerifyPassword(password, foundUser.Password))
                    break;

                errorPassword = "Incorrect password.";
            }

            break;
        }

        Console.Clear();
        Console.WriteLine("==== Login Successful ====\n");
        Console.WriteLine($"Welcome back, {foundUser.UserName}!");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    string ReadInput(bool maskInput, ref string error)
    {
        string input = "";
        ConsoleKeyInfo key;

        int cursorLeft = Console.CursorLeft;
        int cursorTop = Console.CursorTop;
        Console.WriteLine(); // Reserve space for error

        while (true)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(maskInput ? UserLogic.Mask(input) : input);

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

            // Show error box if needed
            Console.SetCursorPosition(0, cursorTop + 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursorTop + 1);

            if (!string.IsNullOrEmpty(error))
            {
                Console.ForegroundColor = ConsoleColor.Red;

                int width = error.Length + 4;
                string topBorder = "  ╭" + new string('─', width) + "╮";
                string middle = $"  │  {error}  │";
                string bottomBorder = "  ╰" + new string('─', width) + "╯";

                Console.WriteLine();
                Console.WriteLine(topBorder);
                Console.WriteLine(middle);
                Console.WriteLine(bottomBorder);

                Console.ResetColor();
                error = "";
            }
        }
    }

    void ShowField(string label, string value, bool isActive, bool? isValid)
    {
        var prefix = isActive ? "> " : "  ";

        if (isValid == true)
            Console.ForegroundColor = ConsoleColor.Green;
        else if (isValid == false)
            Console.ForegroundColor = ConsoleColor.Red;
        else
            Console.ForegroundColor = ConsoleColor.White;

        if (isActive) Console.BackgroundColor = ConsoleColor.DarkGray;

        Console.WriteLine($"{prefix}{label}: {value}");
        Console.ResetColor();
    }
}
