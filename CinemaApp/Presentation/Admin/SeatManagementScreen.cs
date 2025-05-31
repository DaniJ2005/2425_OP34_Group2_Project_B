public class SeatManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Seat Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "Add Seat", "Update Seat", "Delete Seat", "View Seats", "Back" };

        do
        {
            General.ClearConsole();
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
                General.ClearConsole();
                switch (selectedIndex)
                {
                    case 0:
                        SeatConsoleUI.AddSeatUI();
                        break;
                    case 1:
                        SeatConsoleUI.UpdateSeatUI();
                        break;
                    case 2:
                        SeatConsoleUI.DeleteSeatUI();
                        break;
                    case 3:
                        SeatConsoleUI.ViewSeatsUI();
                        break;
                    case 4:
                        MenuLogic.NavigateToPrevious();
                        LoggerLogic.Instance.Log("Returned to previous screen from Seat Management");
                        return;
                } 

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("User pressed Escape - returning to previous screen");
                return;
            }
        } while (true);
    }
}
