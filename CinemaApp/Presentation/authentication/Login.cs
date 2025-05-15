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
            return (CryptoHelper.Verify(input, foundUser.Password), "Incorrect password.");
        }));
    }

    public override void OnFormSubmit()
    {
        UserLogic.Login(Fields[0].Value, Fields[1].Value);
        Console.Clear();
        Console.WriteLine("==== Login Successful ====\n");
        Console.WriteLine($"Welcome back, {foundUser.UserName}!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        MenuLogic.NavigateToPrevious();
    }
}
