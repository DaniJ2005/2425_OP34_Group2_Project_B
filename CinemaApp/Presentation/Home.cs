class Home : IScreen
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public string ScreenName { get; set; }

    public Home() => ScreenName = "Home";

    private string[] options = {
        "Login",
        "Register",
        "Continue without account",
        "Exit"
    };

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            if (UserLogic.CurrentUser?.Id != null)
                Console.WriteLine($"Welcome back, {UserLogic.CurrentUser.UserName}. Thank you for choosing our service.");


            Console.WriteLine("Use ^ V to navigate, Enter to select:\n");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Escape)
            {
                ReservationLogic.ClearSelection();
                MenuLogic.NavigateToPrevious();
            }

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
            {
                selectedIndex++;
            }

        } while (key != ConsoleKey.Enter);

        string[] menuOptions = { "login", "register", "continue without account", "exit" };
        string selectedLabel = (selectedIndex >= 0 && selectedIndex < menuOptions.Length) 
            ? menuOptions[selectedIndex] 
            : "unknown option";
        LoggerLogic.Instance.Log($"User selected | Action: {selectedLabel}");

        Console.Clear();
        switch (selectedIndex)
        {
            case 0: //Login
                // UserLogin.Start();
                break;
            case 1: //Register
                // UserRegister.Start();
                break;
            case 2: //Continue without account
                MenuLogic.NavigateTo(new MovieSelection());
                break;
            case 3:
                MenuLogic.ShowExitConfirmation();
                break; 
        }

        // If not exiting, return to menu
        if (selectedIndex != 3)
        {
            Start();
        }
    }
}
