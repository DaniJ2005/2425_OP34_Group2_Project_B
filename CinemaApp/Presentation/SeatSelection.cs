static class SeatSelection
{
    static string[] seats = {
        "Row A, Seat 1",
        "Row A, Seat 4",
        "Row A, Seat 7",
        "Row C, Seat 4",
        "Row D, Seat 6"
    };

    public static void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Select your seat:\n");

            for (int i = 0; i < seats.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {seats[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {seats[i]}");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0) selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < seats.Length - 1) selectedIndex++;

        } while (key != ConsoleKey.Enter);

        ReservationLogic.SelectedSeat = seats[selectedIndex];
        ConfirmSelection.Start();
    }
}
