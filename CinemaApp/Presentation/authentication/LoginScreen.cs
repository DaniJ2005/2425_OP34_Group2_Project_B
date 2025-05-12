public class LoginScreen : IScreen
{
    public string ScreenName { get; set; }

    public LoginScreen() => ScreenName = "Login";

    string email = "";
    string password = "";
    string errorEmail = "";
    string errorPassword = "";

    User foundUser;

    public void Start()
    {
        User user = Screen();
        if (user == null)
            return;
        
        UserLogic.CurrentUser = user;   
    }

    public User Screen()
    {
        Console.Clear();
        Console.CursorVisible = true;

        email = "";
        password = "";
        errorEmail = "";
        errorPassword = "";
        int topPosition = Console.CursorTop;

        while (true)
        {
            General.ClearConsole(topPosition);
            Console.Clear();
            Console.WriteLine("==== Login Form ====\n");

            ShowField("Email", email, true, string.IsNullOrEmpty(errorEmail) ? null : false);
            ShowField("Password", UserLogic.Mask(password), false, null);


            Console.Write("\n> Email: ");
            email = ReadInput(false, ref errorEmail, (inputEmail) =>
            {
                var user = UserLogic.CheckEmail(inputEmail);
                if (user == null)
                    return "Email not found.";
                foundUser = user;
                return null;
            });
            if (email == null) return null; // user stopped trying

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Login Form ====\n");

                ShowField("Email", email, false, true);

                bool? passwordValid = !string.IsNullOrEmpty(password) ? errorPassword == "" : null;
                ShowField("Password", UserLogic.Mask(password), true, passwordValid);

                Console.Write($"\n> Password: ");
                password = ReadInput(true, ref errorPassword);
                if (password == null) return null;

                if (CryptoHelper.Verify(password, foundUser.Password))
                    break; 

                errorPassword = "Incorrect password.";
            }

            return UserLogic.Login(email, password);
        }
    }

    string ReadInput(bool maskInput, ref string error, Func<string, string?> validateCallback = null)
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
                if (validateCallback != null)
                {
                    string validationError = validateCallback(input);
                    if (validationError != null)
                    {
                        error = validationError;
                        ShowErrorBox(error, cursorTop + 1);
                        continue;
                    }
                }

                return input;
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input += key.KeyChar;
            }

            if (!string.IsNullOrEmpty(error))
            {
                ShowErrorBox(error, cursorTop + 1);
                error = "";
            }
        }
    }

    void ShowErrorBox(string error, int line)
    {
        Console.SetCursorPosition(0, line);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, line);

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
