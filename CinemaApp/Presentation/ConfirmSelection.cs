static class ConfirmSelection
{
    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Please confirm your reservation:\n");
        Console.WriteLine($"Movie Session: {ReservationLogic.SelectedSession}");
        Console.WriteLine($"Seat: {ReservationLogic.SelectedSeat}");

        Console.WriteLine("\nConfirm reservation? (y/n)");
        string input = Console.ReadLine()?.ToLower();

        if (input == "y")
        {
            Console.WriteLine("Reservation confirmed! Enjoy your movie!");
        }
        else
        {
            Console.WriteLine("Reservation cancelled.");
        }
    }
}
