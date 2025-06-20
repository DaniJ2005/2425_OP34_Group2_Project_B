public class Register : FormScreen
{
    public Register()
    {
        ScreenName = "Register";

        Fields.Add(new FormField("Email", false, input =>
            (UserLogic.ValidateEmail(input), "Invalid email format.")));

        Fields.Add(new FormField("Password", true, input =>
            (UserLogic.ValidatePassword(input), "Password must be at least 8 characters.")));

        Fields.Add(new FormField("Repeat Password", true, input =>
        {
            var pass = Fields[1].Value;
            return (input == pass && pass != "", "Passwords do not match.");
        }));

        Fields.Add(new FormField("Full Name", false, input =>
            (UserLogic.ValidateUserName(input), "Full name must be at least 3 characters.")));
    }

    public override void OnFormSubmit()
    {
        string email = Fields[0].Value;
        string password = CryptoHelper.Hash(Fields[1].Value);
        string fullName = Fields[3].Value;

        var newUser = UserLogic.RegisterUser(email, password, fullName);

        General.ClearConsole();
        General.PrintColoredBoxedTitle(newUser != null ? "Registration Complete" : "Registration Failed", 
                                       newUser != null ? ConsoleColor.Green : ConsoleColor.Red);
        Console.WriteLine();


        if (newUser != null)
        {
            Console.Write("Email: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(email);
            Console.ResetColor();

            Console.Write("Full Name: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(fullName);
            Console.ResetColor();
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();

        MenuLogic.NavigateToPrevious();
    }
}
