class HomeScreen : IScreen
{
    public string ScreenName { get; set; }

    public HomeScreen() => ScreenName = "Home";

    private string[] adminOptions = {
        "Book Tickets",
        "admin screen",
        "Login to Account",
        "Create an Account",
        "Exit Application"
    };

    private string[] guestOptions = {
        "Book Tickets",
        "Login to Account",
        "Create an Account",
        "Exit Application"
    };

    private string[] loggedInOptions = {
        "Book Tickets",
        "Logout",
        "Exit Application"
    };

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        bool isLoggedIn = UserLogic.CurrentUser != null;

        string[] options;

        if (isLoggedIn)
        {
            bool isAdmin = UserLogic.CurrentUser.RoleId != 0 || UserLogic.CurrentUser.RoleId != null;
            options = isAdmin ? adminOptions : loggedInOptions;
        }
        else
        {
            options = guestOptions;
        }

        do
        {
            General.ClearConsole();  

            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║          CINEMA APP          ║");
            Console.WriteLine("╚══════════════════════════════╝");


            if (isLoggedIn)
                Console.WriteLine($"Welcome back, {UserLogic.CurrentUser?.UserName}!\n");

            Console.WriteLine("[↓][↑] to navigate\n[ENTER] to confirm your selection.\n");

            for (int i = 0; i < options.Length; i++)
            {
                bool isSelected = i == selectedIndex;

                // Add space above the last item (Exit)
                if (i == options.Length - 1)
                    Console.WriteLine();

                // Highlight selection
                if (isSelected)
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

                if (options[i] == "Book Tickets")
                {
                    Console.WriteLine("  -------------");
                }
            }


            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Escape)
            {
                ReservationLogic.ClearSelection();
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("User pressed Escape - returning to previous menu.");
                return;
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

        string selectedOption = options[selectedIndex];
        LoggerLogic.Instance.Log($"User selected menu option: {selectedOption}");

        Console.Clear();
        switch (selectedOption)
        {
            case "Book Tickets":
                MenuLogic.NavigateTo(new MovieScreen());
                break;

            case "admin screen":
                MenuLogic.NavigateTo(new AdminScreen());
                break;
                
            case "Login to Account":
                MenuLogic.NavigateTo(new Login());
                break;

            case "Create an Account":
                MenuLogic.NavigateTo(new Register());
                break;

            case "Logout":
                LoggerLogic.Instance.Log($"User {UserLogic.CurrentUser?.UserName} logged out.");
                UserLogic.Logout();
                break;

            case "Exit Application":
                MenuLogic.NavigateTo(new ExitScreen());
                return;
        }

        MenuLogic.NavigateTo(new HomeScreen(), true);
    }
}
