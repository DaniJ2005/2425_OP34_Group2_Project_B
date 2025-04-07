static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the registration page");
        Console.WriteLine("Please enter an email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter a password");
        string password = Console.ReadLine();
        Console.WriteLine("Please enter your full name");
        string fullName = Console.ReadLine();
        AccountModel acc = accountsLogic.RegisterAccount(email, password, fullName);
        if (acc != null)
        {
            Console.WriteLine($"Welcome {acc.FullName}, go ahead and login!");
        }
    }
}