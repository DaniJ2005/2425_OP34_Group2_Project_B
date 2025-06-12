public class AdminScreen : IScreen
{
    public string ScreenName { get; set; }

    public AdminScreen()
    {
        ScreenName = "Admin Panel";
    }

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        var permissionOptions = new (bool Permission, string Label)[]
        {
            (UserLogic.CanManageMovieSessions, "Manage Movies"),
            // (UserLogic.CanManageMovieHall, "Manage Seats"),
            // (UserLogic.CanManageAccounts, "Manage Users"),
            // (UserLogic.CanManageGuestAccounts, "Manage Guests"),
            // (UserLogic.CanManageFoodMenu, "Manage Food Menu"),
            // (UserLogic.CanManageReservations, "Manage Reservations")
        };

        string[] menuOptions = permissionOptions
            .Where(po => po.Permission)
            .Select(po => po.Label)
            .Concat(new[] { "Manage Sessions", "Return to Home" })
            .ToArray();


        do
        {
            General.ClearConsole();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║          ADMINISTRATOR         ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Logged in as: ");
            Console.ResetColor();
            Console.WriteLine($"{UserLogic.CurrentUser?.UserName} ({UserLogic.GetRole()})\n");

            Console.WriteLine("[↓][↑] Navigate  [ENTER] Select  [ESC] Back\n");

            for (int i = 0; i < menuOptions.Length; i++)
            {
                bool isSelected = i == selectedIndex;

                if (i == menuOptions.Length - 1)
                    Console.WriteLine();

                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {menuOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {menuOptions[i]}");
                }
            }

            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Escape)
            {
                LoggerLogic.Instance.Log("Admin exited the admin panel.");
                MenuLogic.NavigateToPrevious();
                return;
            }
            else if (key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (key == ConsoleKey.DownArrow && selectedIndex < menuOptions.Length - 1)
            {
                selectedIndex++;
            }
            else if (key == ConsoleKey.Enter)
            {
                string selectedOption = menuOptions[selectedIndex];
                LoggerLogic.Instance.Log($"Admin selected option: {selectedOption}");

                switch (selectedOption)
                {
                    case "Manage Movies":
                        MenuLogic.NavigateTo(new MovieManagementScreen());
                        break;
                    // case "Manage Seats":
                    //     MenuLogic.NavigateTo(new SeatManagementScreen());
                    //     break;
                    case "Manage Users":
                        MenuLogic.NavigateTo(new UserManagementScreen());
                        break;
                    case "Manage Sessions":
                        MenuLogic.NavigateTo(new SessionManagementScreen());
                        break;
                    // case "Manage Guests":
                    //     MenuLogic.NavigateTo(new GuestManagementScreen());
                    //     break;
                    // case "Manage Food Menu":
                    //     MenuLogic.NavigateTo(new FoodMenuManagementScreen());
                    //     break;
                    // case "Manage Reservations":
                    //     MenuLogic.NavigateTo(new ReservationManagementScreen());
                    //     break;
                    case "Return to Home":
                        MenuLogic.NavigateTo(new HomeScreen());
                        return;
                    default:
                        MenuLogic.NavigateTo(new HomeScreen());
                        return;
                }
            }

        } while (true);
    }
}
