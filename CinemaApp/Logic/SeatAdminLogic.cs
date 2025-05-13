public static class SeatAdminLogic
{
    public static List<Seat> GetAllSeats()
    {
        return SeatAccess.GetAllSeats();
    }

    public static List<Seat> GetSeatsByMovieHallId(int movieHallId)
    {
        return SeatAccess.GetAllByMovieHallId(movieHallId);
    }

    public static Seat GetSeatById(int id)
    {
        return SeatAccess.GetSeatById(id);
    }

    public static bool AddSeat(Seat seat)
    {
        try
        {
            SeatAccess.AddSeat(seat);
            LoggerLogic.Instance.Log($"Seat added: Row {seat.Row} Col {seat.Col} in Movie Hall {seat.MovieHallId}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error adding seat: {ex.Message}");
            return false;
        }
    }

    public static bool UpdateSeat(Seat seat)
    {
        try
        {
            SeatAccess.UpdateSeat(seat);
            LoggerLogic.Instance.Log($"Seat updated: {seat.Id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error updating seat: {ex.Message}");
            return false;
        }
    }

    public static bool DeleteSeat(int id)
    {
        try
        {
            SeatAccess.DeleteSeat(id);
            LoggerLogic.Instance.Log($"Seat deleted: {id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error deleting seat: {ex.Message}");
            return false;
        }
    }
}