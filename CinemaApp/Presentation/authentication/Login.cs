public class Login : FormScreen
{
    User foundUser;

    public Login()
    {
        ScreenName = "Login";

        Fields.Add(new FormField("Email", false, input =>
        {
            foundUser = UserLogic.CheckEmail(input);
            return (foundUser != null, "Email not found.");
        }));

        Fields.Add(new FormField("Password", true, input =>
        {
            if (foundUser == null) return (false, "Email must be valid first.");
            return (UserLogic.Login(foundUser.Email, input) != null, "Incorrect password.");
        }));
    }

    public override void OnFormSubmit()
    {
        UserLogic.Login(Fields[0].Value, Fields[1].Value);
        General.ClearConsole();

        General.PrintColoredBoxedTitle("Login Successful", ConsoleColor.Green);

        Console.WriteLine();

        Console.Write("Welcome back, ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(foundUser.UserName);
        Console.ResetColor();
        Console.WriteLine("!");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();

        MenuLogic.NavigateToPrevious();
    }
}
