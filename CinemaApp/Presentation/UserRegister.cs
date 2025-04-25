static class UserRegister
{
    static private UserLogic accountsLogic = new UserLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the registration page");
        Console.WriteLine("Please enter an email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter a password");
        string password = Console.ReadLine();
        Console.WriteLine("Please enter your full name");
        string userName = Console.ReadLine();
        User user = accountsLogic.RegisterUser(email, password, userName);
        if (UserLogic.CurrentUser != null)
        {
            Console.WriteLine($"Welcome {user.UserName}, go ahead and login!");
        }
    }
}