class HomeScreen : IScreen
{
    public string ScreenName { get; set; }

    public HomeScreen() => ScreenName = "Home";

    private readonly string[] _guestOptions = {
        "Book Tickets",
        "Login to Account",
        "Create an Account",
        "Exit Application"
    };

    private readonly string[] _userOptions = {
        "Book Tickets",
        "View Reservations",
        "Logout",
        "Exit Application"
    };

    private readonly string[] _adminOptions = {
        "Book Tickets",
        "Admin Panel",
        "Logout",
        "Exit Application"
    };

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        bool isAuthenticated = UserLogic.IsAuthenticated();
        bool isAdmin = UserLogic.IsAuthenticated() && UserLogic.IsAdmin();
        string[] options = isAdmin ? _adminOptions :
                           isAuthenticated ? _userOptions : _guestOptions;

        do
        {
            General.ClearConsole();

            if (isAdmin)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (isAuthenticated)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║          CINEMA APP          ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();



                if (isAuthenticated)
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

            General.ClearConsole();
            switch (selectedOption)
            {
                case "Book Tickets":
                    MenuLogic.NavigateTo(new MovieScreen());
                    break;

                case "View Reservations":
                    MenuLogic.NavigateTo(new ViewAllReservationsScreen());
                    break;

                case "Admin Panel":
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
