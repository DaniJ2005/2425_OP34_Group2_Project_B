using System.Threading.Tasks.Dataflow;

static class ConfirmSelection
{
    //commented is waiting for logic of diffrent git-branch
    private static readonly string[] menuItems = {
        "Add food",
        "Return to previous page",
        "Confirm reservation"
    };

    public static void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Please confirm your reservation:\n");
            // Console.WriteLine($"Movie Session: {ReservationLogic.GetConfirmationSummary()}");

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"> {menuItems[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {menuItems[i]}");
                }
            }

            Console.WriteLine("\nUse ^ v to navigate, [Enter] to select.");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < menuItems.Length - 1)
                selectedIndex++;
        }
        while (key != ConsoleKey.Enter);

        Console.Clear();

        switch (selectedIndex)
        {
            case 0: // Add food
                Console.WriteLine("You chose to add food. (Redirecting...)");

                Console.WriteLine("Press any key to return...");
                Console.ReadKey(true);
                Start();
                return;

            case 1: // Return
                SeatSelection.Start();
                Start();
                break;

            case 2: // Confirm reservation
                string email;

                do
                {
                    Console.WriteLine("Please enter an email address, so we can send you the ticket!");
                    email = Console.ReadLine();

                    if (!UserLogic.IsValidEmail(email))
                    {
                        Console.WriteLine("- The email address is invalid.\n");
                    }

                } while (!UserLogic.IsValidEmail(email));

                Console.WriteLine($"\nReservation confirmed! Confirmation sent to {email}.");
                // ReservationLogic.Confirm(email);
                break;
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }
}

