public static class SeatConsoleUI
{
    public static void AddSeatUI()
    {
        Console.WriteLine("Enter Movie Hall ID:");
        int hallId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter total rows in this hall:");
        int rows = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter total columns in this hall:");
        int cols = int.Parse(Console.ReadLine());

        var pos = NavigateSeatPosition(hallId, rows, cols);
        if (pos == null)
        {
            Console.WriteLine("Seat addition cancelled.");
            return;
        }

        Console.WriteLine("Enter Seat Type ID:");
        int seatTypeId = int.Parse(Console.ReadLine());

        Console.WriteLine("Is the seat under maintenance? (true/false):");
        bool maintenance = bool.Parse(Console.ReadLine());

        Seat seat = new Seat
        {
            MovieHallId = hallId,
            Row = pos.Value.Row,
            Col = pos.Value.Col,
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

        Console.WriteLine($"Current Row: {seat.Row}, Current Col: {seat.Col}");
        Console.WriteLine("Do you want to change the seat position? (y/n):");
        string changePosition = Console.ReadLine().ToLower();

        if (changePosition == "y")
        {
            Console.WriteLine("Enter total rows in this hall:");
            int rows = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter total columns in this hall:");
            int cols = int.Parse(Console.ReadLine());

            var newPos = NavigateSeatPosition(seat.MovieHallId, rows, cols);
            if (newPos == null)
            {
                Console.WriteLine("Seat position update cancelled.");
                return;
            }

            seat.Row = newPos.Value.Row;
            seat.Col = newPos.Value.Col;
        }

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
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                               SEATS LIST                              ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
        Console.WriteLine("Press -> to scroll up, <- to scroll down, or [Esc] to go back.");


        var table = new Table<Seat>(maxColWidth: 20, pageSize: 10);
        table.SetColumns("Id", "MovieHallId", "Row", "Col", "SeatTypeId", "IsUnderMaintenance");
        table.AddRows(seats);
        table.Print("ID", "Hall", "Row", "Col", "Type", "Maintenance");
        if ((Console.ReadKey(true).Key != ConsoleKey.RightArrow) && (Console.ReadKey(true).Key != ConsoleKey.LeftArrow))
        {
            Console.WriteLine("Invalid key. Please use the arrow keys to scroll.");
        }
        else if (Console.ReadKey(true).Key == ConsoleKey.RightArrow)
        {
            table.NextPage();
        }
        else if (Console.ReadKey(true).Key == ConsoleKey.LeftArrow)
        {
            table.PreviousPage();
        }
        
        else
        {
            Console.WriteLine("No more seats to display.");
        }
        Console.WriteLine("Press esc to go back.");
        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {
            
        }
        Console.Clear();
        Console.WriteLine("Returned to previous screen.");
    }

    private static (int Row, int Col)? NavigateSeatPosition(int movieHallId, int rows, int cols)
    {
        int cursorRow = 0;
        int cursorCol = 0;

        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.WriteLine($"Movie Hall ID: {movieHallId}");
            Console.WriteLine("Use arrow keys to choose seat position. [Enter] to confirm, [Esc] to cancel.\n");

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (r == cursorRow && c == cursorCol)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write("[X]");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("[ ]");
                    }
                }
                Console.WriteLine();
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && cursorRow > 0) cursorRow--;
            else if (key == ConsoleKey.DownArrow && cursorRow < rows - 1) cursorRow++;
            else if (key == ConsoleKey.LeftArrow && cursorCol > 0) cursorCol--;
            else if (key == ConsoleKey.RightArrow && cursorCol < cols - 1) cursorCol++;
            else if (key == ConsoleKey.Escape) return null;
            else if (key == ConsoleKey.Enter) break;

        } while (true);

        return (cursorRow, cursorCol);
    }
}
