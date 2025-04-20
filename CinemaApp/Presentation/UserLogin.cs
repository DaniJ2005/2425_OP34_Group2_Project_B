static class UserLogin
{
    static private UserLogic userLogic = new UserLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        UserModel user = userLogic.CheckLogin(email, password);

        if (UserLogic.CurrentUser != null)
        {
            Console.WriteLine("Welcome back " + user.UserName);
            Console.WriteLine("Your email number is " + user.Email);

            //if (role == "admin")
            ShowAdminPrompt();
        }
        else
        {
            Console.WriteLine("No user found with that email and password");
        }
    }

    public static void ShowAdminPrompt()
    {
        Console.WriteLine("\nYou are an admin, would you like to see all users? (y/n)");

        string choice = Console.ReadLine();
        if (choice?.ToLower() == "y")
        {
            ShowAllUsers();
        }
        else
        {
            Console.WriteLine("You chose not to view the users.");
        }
    }

    public static void ShowAllUsers()
    {
        Console.Clear();
        Console.WriteLine("\nAll users:");
        List<UserModel> users = UserAccess.GetAllUsers();

        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Full Name: {user.UserName}");
            Console.WriteLine("------------------------------------");
        }
    }
}