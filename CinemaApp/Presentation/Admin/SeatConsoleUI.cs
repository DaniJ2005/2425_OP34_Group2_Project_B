public static class SeatConsoleUI
{
    public static void AddSeatUI()
    {
        Console.WriteLine("Enter Movie Hall ID:");
        int hallId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Row:");
        int row = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Column:");
        int col = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Seat Type ID:");
        int seatTypeId = int.Parse(Console.ReadLine());

        Console.WriteLine("Is the seat under maintenance? (true/false):");
        bool maintenance = bool.Parse(Console.ReadLine());

        Seat seat = new Seat
        {
            MovieHallId = hallId,
            Row = row,
            Col = col,
            SeatTypeId = seatTypeId,
            IsUnderMaintenance = maintenance
        };

        if (SeatAdminLogic.AddSeat(seat))
            Console.WriteLine("Seat added successfully!");
        else
            Console.WriteLine("Failed to add seat.");
    }
 
    public static void UpdateSeatUI()
    {
        Console.WriteLine("Enter Seat ID to update:");
        int id = int.Parse(Console.ReadLine());

        Seat seat = SeatAdminLogic.GetSeatById(id);
        if (seat == null)
        {
            Console.WriteLine("Seat not found.");
            return;
        }

        Console.WriteLine($"Current Row: {seat.Row}, New Row:");
        seat.Row = int.Parse(Console.ReadLine());

        Console.WriteLine($"Current Col: {seat.Col}, New Col:");
        seat.Col = int.Parse(Console.ReadLine());

        Console.WriteLine($"Current MovieHallId: {seat.MovieHallId}, New MovieHallId:");
        seat.MovieHallId = int.Parse(Console.ReadLine());

        Console.WriteLine($"Current SeatTypeId: {seat.SeatTypeId}, New SeatTypeId:");
        seat.SeatTypeId = int.Parse(Console.ReadLine());

        Console.WriteLine($"Under maintenance? (true/false):");
        seat.IsUnderMaintenance = bool.Parse(Console.ReadLine());

        if (SeatAdminLogic.UpdateSeat(seat))
            Console.WriteLine("Seat updated successfully!");
        else
            Console.WriteLine("Failed to update seat.");
    }

    public static void DeleteSeatUI()
    {
        Console.WriteLine("Enter Seat ID to delete:");
        int id = int.Parse(Console.ReadLine());

        if (SeatAdminLogic.DeleteSeat(id))
            Console.WriteLine("Seat deleted successfully.");
        else
            Console.WriteLine("Failed to delete seat.");
    }

    public static void ViewSeatsUI()
    {
        var seats = SeatAdminLogic.GetAllSeats();
        if (seats.Count == 0)
        {
            Console.WriteLine("No seats found.");
            return;
        }

        foreach (var seat in seats)
        {
            Console.WriteLine($"ID: {seat.Id}, Hall: {seat.MovieHallId}, Row: {seat.Row}, Col: {seat.Col}, Type: {seat.Type}, Maintenance: {seat.IsUnderMaintenance}");
        }
    }
}
