static class MovieSessionSelection
{
    static string[] sessions = {
        "A Minecraft Movie - Auditorium 3 - 15:00",
        "Peppa Big - Auditorium 1 - 16:00",
        "The Amateur - Auditorium 2 - 18:30",
        "Novocaine - Auditorium 3 - 20:15"
    };

    public static void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Select a movie session:\n");

            for (int i = 0; i < sessions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {sessions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {sessions[i]}");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0) selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < sessions.Length - 1) selectedIndex++;

        } while (key != ConsoleKey.Enter);

        ReservationLogic.SelectedSession = sessions[selectedIndex];
        SeatSelection.Start();
    }
}
