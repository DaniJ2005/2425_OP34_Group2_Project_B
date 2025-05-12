using System;

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

        string[] options = {
            "Manage Movies",
            "Manage Seats",
            "Manage Users",
            "View Booking Analytics",
            "Return to Home"
        };

        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║          ADMIN PANEL         ║");
            Console.WriteLine("╚══════════════════════════════╝");
            
            Console.WriteLine($"Logged in as: {UserLogic.CurrentUser?.UserName} (Admin)\n");
            Console.WriteLine("[↓][↑] to navigate\n[ENTER] to select\n[ESC] to go back\n");

            for (int i = 0; i < options.Length; i++)
            {
                bool isSelected = i == selectedIndex;

                // Add space above the last item (Return to Home)
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
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("Admin exited the admin panel.");
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
            else if (key == ConsoleKey.Enter)
            {
                string selectedOption = options[selectedIndex];
                LoggerLogic.Instance.Log($"Admin selected option: {selectedOption}");

                switch (selectedIndex)
                {
                    case 0: // Manage Movies
                        MenuLogic.NavigateTo(new MovieManagementScreen());
                        break;
                    case 1: // Manage Seats
                        MenuLogic.NavigateTo(new SeatManagementScreen());
                        break;
                    case 2: // Manage Users
                        MenuLogic.NavigateTo(new UserManagementScreen());
                        break;
                    case 3: // Return to Home
                        MenuLogic.NavigateTo(new HomeScreen());
                        return;
                }
            }

        } while (key != ConsoleKey.Enter || selectedIndex != 4);
    }
}