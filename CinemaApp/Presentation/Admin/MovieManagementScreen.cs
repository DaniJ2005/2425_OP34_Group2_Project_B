using System;

public class MovieManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Movie Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "Add Movie", "Update Movie", "Delete Movie", "View Movies", "Back" };
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       MOVIE MANAGEMENT       ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.WriteLine("[↑][↓] to navigate, [ENTER] to select, [ESC] to go back\n");
            
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
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0:
                        //MovieAdminLogic.AddMovie();
                        break;
                    case 1:
                        //MovieAdminLogic.UpdateMovie();
                        break;
                    case 2:
                        //MovieAdminLogic.DeleteMovie();
                        break;
                    case 3:
                        //MovieAdminLogic.ViewMovies();
                        break;
                    case 4:
                        MenuLogic.NavigateToPrevious();
                        LoggerLogic.Instance.Log("User returned to admin menu from movie management");
                        return;
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("User pressed Escape - returning to admin menu");
                return;
            }
        } while (true);
    }
}