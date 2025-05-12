static class SeatLogic
{
    public static Seat[,] SeatGrid;
    public static HashSet<int> BookedSeatIds = new();
    public static HashSet<int> SelectedSeatIds = new();

    public static int X = 0;
    public static int Y = 0;

    public static void LoadSeats(MovieSession movieSession)
    {
        // Reset HashSet's
        BookedSeatIds.Clear();

        X = 0;
        Y = 0;

        List<Seat> seatList = SeatAccess.GetAllByMovieHallId(movieSession.MovieHallId);
        List<Ticket> existingTickets = TicketAccess.GetTicketsByMovieSessionId(movieSession.Id);

        foreach (Ticket ticket in existingTickets)
        {
            BookedSeatIds.Add(ticket.SeatId);
        }

        int maxRow = seatList.Max(s => s.Row);
        int maxCol = seatList.Max(s => s.Col);

        SeatGrid = new Seat[maxRow, maxCol];

        foreach (Seat seat in seatList)
        {
            SeatGrid[seat.Row - 1, seat.Col - 1] = seat;
        }

    }

    public static void ToggleSeatSelection()
    {
        Seat currentSeat = SeatGrid[Y, X];
        if (currentSeat == null) return;

        if (SelectedSeatIds.Contains(currentSeat.Id))
        {
            SelectedSeatIds.Remove(currentSeat.Id);
        }
        else
        {
            SelectedSeatIds.Add(currentSeat.Id);
        }
    }

    public static void StoreSelection(MovieSession movieSession)
    {
        List<Seat> seatList = SeatAccess.GetAllByMovieHallId(movieSession.MovieHallId);
        List<Seat> SelectedSeatList = [];

        foreach (Seat seat in seatList)
        {
            if (SelectedSeatIds.Contains(seat.Id))
            {
                SelectedSeatList.Add(seat);
            }
        }

        ReservationLogic.SetSelectedSeats(SelectedSeatList);
    }

    public static void MoveUp()
    {
        for (int newY = Y - 1; newY >= 0; newY--)
        {
            if (IsValidSeat(newY, X))
            {
                Y = newY;
                return;
            }
        }
    }

    public static void MoveDown()
    {
        for (int newY = Y + 1; newY < SeatGrid.GetLength(0); newY++)
        {
            if (IsValidSeat(newY, X))
            {
                Y = newY;
                return;
            }
        }
    }

    public static void MoveLeft()
    {
        for (int newX = X - 1; newX >= 0; newX--)
        {
            if (IsValidSeat(Y, newX))
            {
                X = newX;
                return;
            }
        }
    }

    public static void MoveRight()
    {
        for (int newX = X + 1; newX < SeatGrid.GetLength(1); newX++)
        {
            if (IsValidSeat(Y, newX))
            {
                X = newX;
                return;
            }
        }
    }

    private static bool IsValidSeat(int row, int col)
    {
        var seat = SeatGrid[row, col];
        return seat != null && !BookedSeatIds.Contains(seat.Id) && !seat.IsUnderMaintenance;
    }

}   