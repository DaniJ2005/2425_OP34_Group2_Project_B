public class ExitLogoutScreen : IScreen
{
    public string ScreenName { get; set; }

    private readonly bool _isLogoutOnly;

    public ExitLogoutScreen(bool isLogoutOnly)
    {
        _isLogoutOnly = isLogoutOnly;
        ScreenName = isLogoutOnly ? "Logout" : "Exit Application";
    }

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        string prompt = _isLogoutOnly
            ? "Are you sure you want to logout?"
            : "Are you sure you want to exit?";

        List<string> options = new List<string> { "Cancel", "Confirm" };

        if (!_isLogoutOnly && UserLogic.IsAuthenticated())
            options.Add("Confirm and Logout");
        

        do
        {
            General.ClearConsole();
            Console.WriteLine($"{prompt}\n");

            for (int i = 0; i < options.Count; i++)
            {
                bool isSelected = (i == selectedIndex);

                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"[ {options[i]} ]");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"  {options[i]}  ");
                }

                Console.Write("   ");
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.RightArrow && selectedIndex < options.Count - 1)
                selectedIndex++;

        } while (key != ConsoleKey.Enter);

        General.ClearConsole();

        string selectedOption = options[selectedIndex];

        switch (selectedOption)
        {
            case "Cancel":
                MenuLogic.NavigateToPrevious();
                break;

            case "Confirm":
                if (_isLogoutOnly)
                {
                    LoggerLogic.Instance.Log($"User {UserLogic.CurrentUser?.UserName} logged out.");
                    UserLogic.Logout();
                    MenuLogic.NavigateTo(new HomeScreen());
                }
                else
                {
                    Console.WriteLine("Exiting program...");
                    Console.CursorVisible = true;
                    Environment.Exit(0);
                }
                break;

            case "Confirm and Logout":
                LoggerLogic.Instance.Log($"User {UserLogic.CurrentUser?.UserName} logged out and exited.");
                UserLogic.Logout();
                Console.WriteLine("Exiting program...");
                Console.CursorVisible = true;
                Environment.Exit(0);
                break;
        }
    }
}
