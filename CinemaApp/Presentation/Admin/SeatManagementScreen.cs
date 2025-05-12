using System;

public class SeatManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Seat Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = {  "Add Seat", "Update Seat", "Delete Seat", "View Seats", "Back" };
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       SEAT MANAGEMENT        ║");
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
                        //SeatAdminLogic.AddSeat();
                        break;
                    case 1:
                        //SeatAdminLogic.UpdateSeat();
                        break;
                    case 2:
                        //SeatAdminLogic.ViewMovies();
                        break;
                    case 3:
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