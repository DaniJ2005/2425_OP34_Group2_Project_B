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
        bool isAdmin = UserLogic.IsAdmin();
        string[] options = isAdmin ? _adminOptions :
                           isAuthenticated ? _userOptions : _guestOptions;

        ConsoleColor boxColor = isAdmin ? ConsoleColor.Yellow :
                                 isAuthenticated ? ConsoleColor.Green : ConsoleColor.Cyan;

        do
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle("Cinema app", boxColor, true);
            Console.WriteLine();

            if (isAuthenticated)
            {
                Console.Write("Welcome back, ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(UserLogic.CurrentUser?.UserName);
                Console.ResetColor();
                Console.WriteLine("!\n");
            }
            else
            {
                Console.WriteLine("Welcome, guest!\n");
            }

            Console.WriteLine("[↓][↑] Navigate   [ENTER] Confirm   [ESC] Exit\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == 0) Console.WriteLine("────────────");

                bool isSelected = (i == selectedIndex);
                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }

                if (i == 0) Console.WriteLine("────────────");

                if (i == options.Length - 2) Console.WriteLine(); // Space before Exit
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Escape)
            {
                ReservationLogic.ClearSelection();
                MenuLogic.NavigateTo(new ExitLogoutScreen(false));
                return;
            }

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;

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
                MenuLogic.NavigateTo(new ExitLogoutScreen(true));
                break;
            case "Exit Application":
                MenuLogic.NavigateTo(new ExitLogoutScreen(false));
                return;
        }

        MenuLogic.NavigateTo(new HomeScreen(), true);
    }
}
