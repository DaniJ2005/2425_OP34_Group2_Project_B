class ConfirmSelectionScreen : IScreen
{
    private readonly string[] menuItems = {
        "Add food",
        "Confirm reservation"
    };
    public string ScreenName { get; set; }
    public ConfirmSelectionScreen() => ScreenName = "Confirmation";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        LoggerLogic.Instance.Log($"Confirm reservation | Summery:\n{ReservationLogic.GetConfirmationSummary()}");
        int topPosition = Console.CursorTop;

        do
        {
            General.ClearConsole(topPosition);  
            Console.WriteLine("Please confirm your reservation:\n");
            Console.WriteLine(ReservationLogic.GetConfirmationSummary());

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

            Console.WriteLine("\n\nUse ^ v to navigate, [Enter] to select and [Escape] to cancel.");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
            }
            else if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < menuItems.Length - 1)
                selectedIndex++;
        }
        while (key != ConsoleKey.Enter);

        string[] menuOptions = { "Add food", "Confirm reservation" };
        string selectedLabel = (selectedIndex >= 0 && selectedIndex < menuOptions.Length) 
            ? menuOptions[selectedIndex] 
            : "unknown option";
        LoggerLogic.Instance.Log($"User selected | Action: {selectedLabel}");

        Console.Clear();

        switch (selectedIndex)
        {
            case 0: // Add food
                Console.WriteLine("You chose to add food. (Redirecting...)");
                MenuLogic.NavigateTo(new FoodScreen());
                Console.WriteLine("Press any key to return...");
                Console.ReadKey(true);
                Start();
                return;

            case 1: // Confirm reservation
                string email;
                do
                {
                    Console.WriteLine("Please enter an email address, so we can send you the ticket!");
                    email = Console.ReadLine();

                    if (!UserLogic.ValidateEmail(email))
                    {
                        Console.WriteLine("- The email address is invalid.\n");
                    }

                } while (!UserLogic.ValidateEmail(email));

                ReservationLogic.CreateReservation(email);

                Console.WriteLine($"\nReservation confirmed! Confirmation sent to {email}.\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                // ReservationLogic.Confirm(email);
                MenuLogic.NavigateTo(new HomeScreen(), true);

                LoggerLogic.Instance.Log($"User confirmed email | Address: {email}");
                break;
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }
}

