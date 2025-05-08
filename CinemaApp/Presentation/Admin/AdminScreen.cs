public class AdminScreen : IScreen
{
    public string ScreenName { get; set; }

    public void Start()
    {
        Console.WriteLine("Welcome to the admin screen");
        Console.WriteLine("Use ^ V to navigate, Enter to select:\n");
        string[] options = {
            "Manage Movies",
            "Manage Seats",
            "Manage Users",
            "View Booking Analytics",
            "Exit"
        };
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
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

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        ManageMovies();
                        break;
                    case 1:
                        ManageSeats();
                        break;
                    case 2:
                        ManageUsers();
                        break;
                    case 3:
                        ViewBookingAnalytics();
                        break;
                    case 4:
                        MenuLogic.NavigateToPrevious();
                        break;
                }
            }
        } while (key != ConsoleKey.Escape);
    }

    public void ManageMovies()
    {
        // Add, update, or delete movies
        Console.WriteLine("Manage Movies");
        Console.WriteLine("1. Add Movie");
        Console.WriteLine("2. Update Movie");
        Console.WriteLine("3. Delete Movie");
        Console.WriteLine("4. View Movies");
        Console.WriteLine("5. Back to Admin Menu");
        ConsoleKey key;
        int selectedIndex = 0;
        string[] options = { "Add Movie", "Update Movie", "Delete Movie", "View Movies", "Back" };
        do
        {
            Console.Clear();
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

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        // Add movie logic
                        break;
                    case 1:
                        // Update movie logic
                        break;
                    case 2:
                        // Delete movie logic
                        break;
                    case 3:
                        // View movies logic
                        break;
                    case 4:
                        return; // Back to Admin Menu
                }
            }
        } while (key != ConsoleKey.Escape);
    }

    public void ManageSeats()
    {
        // Add, update, or delete seats
        Console.WriteLine("Manage Seats");
        Console.WriteLine("1. Add Seat");
        Console.WriteLine("2. Update Seat");
        Console.WriteLine("3. Delete Seat");
        Console.WriteLine("4. View Seats");
        Console.WriteLine("5. Back to Admin Menu");
        ConsoleKey key;
        int selectedIndex = 0;
        string[] options = { "Add Seat", "Update Seat", "Delete Seat", "View Seats", "Back" };
        do
        {
            Console.Clear();
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

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        // Add seat logic
                        break;
                    case 1:
                        // Update seat logic
                        break;
                    case 2:
                        // Delete seat logic
                        break;
                    case 3:
                        // View seats logic
                        break;
                    case 4:
                        return; // Back to Admin Menu
                }
            }
        } while (key != ConsoleKey.Escape);
        Console.WriteLine("Press any key to return to the admin menu...");
        Console.ReadKey();
        Console.Clear();
        Start(); // Restart the admin menu

    }

    public void ManageUsers()
    {
        // Add, update, or delete users
        Console.WriteLine("Manage Users");
        Console.WriteLine("1. Add User");
        Console.WriteLine("2. Update User");
        Console.WriteLine("3. Delete User");
        Console.WriteLine("4. View Users");
        Console.WriteLine("5. Back to Admin Menu");
        ConsoleKey key;
        int selectedIndex = 0;
        string[] options = { "Add User", "Update User", "Delete User", "View Users", "Back" };
        do
        {
            Console.Clear();
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

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        // Add user logic
                        break;
                    case 1:
                        // Update user logic
                        break;
                    case 2:
                        // Delete user logic
                        break;
                    case 3:
                        // View users logic
                        break;
                    case 4:
                        return; // Back to Admin Menu
                }
            }
        } while (key != ConsoleKey.Escape);

    }

    public void ViewBookingAnalytics()
    {
        // Display booking analytics
        Console.WriteLine("Booking Analytics");
        Console.WriteLine("1. View Booking Statistics");
        Console.WriteLine("2. View Booking History");
        Console.WriteLine("3. View Most Popular Movies");
        Console.WriteLine("4. View Most Popular Movie Halls");
        Console.WriteLine("5. Back to Admin Menu");
        ConsoleKey key;
        int selectedIndex = 0;
        string[] options = { "View Booking Statistics", "View Booking History", "View Most Popular Movies", "View Most Popular Movie Halls", "Back" };        
        do
        {
            Console.Clear();
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

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedIndex)
                {
                    case 0:
                        // View booking statistics logic
                        break;
                    case 1:
                        // View booking history logic
                        break;
                    case 2:
                        // View most popular movies logic
                        break;
                    case 3:
                        // View most popular movie halls logic
                        break;
                    case 4:
                        return; // Back to Admin Menu
                }
            }
        } while (key != ConsoleKey.Escape);
        Console.WriteLine("Press any key to return to the admin menu...");
        Console.ReadKey();
        Console.Clear();
        Start(); // Restart the admin menu
    }
}