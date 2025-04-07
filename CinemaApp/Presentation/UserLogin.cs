static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            //if (role == "admin")
            ShowAdminPrompt();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }

    public static void ShowAdminPrompt()
    {
        Console.WriteLine("\nYou are an admin, would you like to see all accounts? (y/n)");

        string choice = Console.ReadLine();
        if (choice?.ToLower() == "y")
        {
            ShowAllAccounts();
        }
        else
        {
            Console.WriteLine("You chose not to view the accounts.");
        }
    }

    public static void ShowAllAccounts()
    {
        Console.Clear();
        Console.WriteLine("\nAll accounts:");
        List<AccountModel> accounts = AccountsAccess.GetAllAccounts();

        foreach (var account in accounts)
        {
            Console.WriteLine($"ID: {account.Id}");
            Console.WriteLine($"Email: {account.EmailAddress}");
            Console.WriteLine($"Full Name: {account.FullName}");
            Console.WriteLine("------------------------------------");
        }
    }
}