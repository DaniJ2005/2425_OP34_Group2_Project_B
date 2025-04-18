static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    static string[] options = {
        "Login",
        "Register",
        "Continue without account",
        "Exit"
    };

    static public void Start()
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

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
            {
                selectedIndex++;
            }

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        switch (selectedIndex)
        {
            case 0: //Login
                UserLogin.Start();
                break;
            case 1: //Register
                UserRegister.Start();
                break;
            case 2: //Continue without account
                MovieSelection.Start();
                MovieSession.Start();
                // ConfirmSelection.Start();

                // Temporary code for testing movie session result
                MovieModel movie = ReservationLogic.GetSelectedMovie();
                MovieSessionModel movieSession = ReservationLogic.GetSelectedSession();
                Console.Clear();
                Console.WriteLine($"Movie: {movie.Title}, Date: {movieSession.Date}, Time: {movieSession.StartTime}");

                break;
            case 3:
                Console.WriteLine("Exiting...");
                break; //Exit
        }

        // If not exiting, return to menu
        if (selectedIndex != 3)
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(true);
            Start();
        }
    }
}
