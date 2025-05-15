public class ExitScreen : IScreen
{
    public string ScreenName { get; set; }
    public ExitScreen()
    {
        ScreenName = "";
    }
    public void Start()
    {
        Screen();
    }

    private void Screen()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            General.ClearConsole();  
            Console.WriteLine("Are you sure you want to exit?\n");

            if (selectedIndex == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Cancel ]");
                Console.ResetColor();
                Console.Write("   Exit   ");
            }
            else
            {
                Console.Write("  Cancel   ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Exit ]");
                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.RightArrow && selectedIndex < 1)
                selectedIndex++;

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        if (selectedIndex == 1)
        {
            Console.WriteLine("Exiting program...");
            Environment.Exit(0);
        }
        else 
        {
            MenuLogic.NavigateToPrevious();
        }
    }
}