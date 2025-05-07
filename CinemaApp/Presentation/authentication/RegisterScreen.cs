using System.Security.Cryptography;

public class RegisterScreen : IScreen
{
    public string ScreenName { get; set; }

    public RegisterScreen() => ScreenName = "Register";

    string email = "";
    string password = "";
    string passwordRepeat = "";
    string fullName = "";

    public void Start() => Start(false);
    public void Start(bool repeatError)
    {
        Console.Clear();
        Console.CursorVisible = true;

        if (repeatError == false)
            email = ReadField("Email", UserLogic.ValidateEmail, "Invalid email format.", false);
        if (email == null) 
            return;

        password = ReadField("Password", UserLogic.ValidatePassword, "Password must be at least 8 characters.", true);
        if (password == null) 
            return;

        passwordRepeat = ReadField("Repeat Password", (input) => input == password, "Passwords do not match.", true);
        if (passwordRepeat == null) 
            return;

        fullName = ReadField("Full Name", UserLogic.ValidateUserName, "Full name must be at least 3 characters.", false);
        if (fullName == null) 
            return;

        string hashedPassword = HashPassword(password);
        var newUser = UserLogic.RegisterUser(email, hashedPassword, fullName);

        Console.Clear();
        if (newUser != null)
        {
            Console.WriteLine("==== Registration Complete ====\n");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Full Name: {fullName}");
            Console.WriteLine("Registration successful.");
        }
        else
        {
            Console.WriteLine("==== Registration Failed ====\n");
            Console.WriteLine("There was an error during registration. Please check the entered data.");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    string ReadField(string label, Func<string, bool> validate, string errorMessage, bool maskInput)
    {
        string input = "";
        ConsoleKeyInfo key;
        string error = "";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Registration Form ==== (Press Esc to cancel)\n");

            ShowField("Email", email, UserLogic.ValidateEmail(email), label == "Email");
            ShowField("Password", UserLogic.Mask(password), UserLogic.ValidatePassword(password), label == "Password");
            ShowField("Repeat Password", UserLogic.Mask(passwordRepeat), passwordRepeat == password && password != "", label == "Repeat Password");
            ShowField("Full Name", fullName, UserLogic.ValidateUserName(fullName), label == "Full Name");

            Console.Write($"\n> {label} : ");
            input = "";
            error = "";

            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Console.WriteLine(); // Reserve line for error

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
                    if (validate(input))
                        return input;
                    else if (label == "Repeat Password")
                    {
                        password = "";
                        Start(true);
                    }
                    else
                        error = errorMessage;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                }

                // Show or clear error message
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
    }

    private string HashPassword(string password)
    {
        // Make a random salt (like a secret ingredient)
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[16]; // 16 bytes = enough salt
        rng.GetBytes(salt); // Fill it with random values

        // Mix the password and salt using PBKDF2 to make it safe
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32); // Get 32 bytes of strong, scrambled password

        // Create space to hold both salt and password hash
        byte[] hashBytes = new byte[48];

        // Copy the salt into the first part
        Array.Copy(salt, 0, hashBytes, 0, 16);

        // Copy the hashed password into the second part
        Array.Copy(hash, 0, hashBytes, 16, 32);

        // Turn the full result into a single string to store safely
        return Convert.ToBase64String(hashBytes);
    }


    void ShowField(string label, string value, bool isValid, bool isActive)
    {
        var prefix = isActive ? "> " : "  ";
        Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
        if (isActive) Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{prefix}{label}: {value}");
        Console.ResetColor();
    }
}
