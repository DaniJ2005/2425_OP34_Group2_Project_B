static class ReservationLogic
{
    private static Movie _selectedMovie;
    private static MovieSession _selectedSession;
    private static List<Seat> _selectedSeats = [];

    public static void SetSelectedMovie(Movie movie)
    {
        LoggerLogic.Instance.Log($"Movie selected | ID: {movie.Id} | Title: {movie.Title}");
        _selectedMovie = movie;
    }
    public static void SetSelectedSession(MovieSession session)
    {
        LoggerLogic.Instance.Log($"Session selected | ID: {session.Id} | MovieHallID: {session.MovieHallId} | Date: {session.Date}");
        _selectedSession = session;
    }
    public static void SetSelectedSeats(List<Seat> seats)
    {
        foreach (var seat in seats)
        {
            LoggerLogic.Instance.Log($"Movie Seat | ID: {seat.Id} | Type: {seat.SeatTypeId}");
        }
        _selectedSeats = new List<Seat>(seats);
    }

    public static Movie GetSelectedMovie() => _selectedMovie;
    public static MovieSession GetSelectedSession() => _selectedSession;
    public static List<Seat> GetSelectedSeats() => _selectedSeats;

    public static string GetConfirmationSummary()
    {
        if (_selectedMovie == null) //When session en seat are implemented add null check for 
            return "No reservation selected.";

        string summary = "";

        if (UserLogic.CurrentUser != null && !string.IsNullOrEmpty(UserLogic.CurrentUser.UserName))
        {
            summary += $"User: {UserLogic.CurrentUser.UserName}\n";
        }

        summary +=
            $"  - Movie: {GetSelectedMovie().Title}   ({GetSelectedMovie().Duration})\n" +
            $"  - Date: {GetSelectedSession().Date}\n" +
            $"  - Time: {GetSelectedSession().StartTime}\n" +
            $"  - Hall: {GetSelectedSession().MovieHallId}\n"; //+
            // $"  - Seat: Row {GetSelectedSeat().Row}, Seat {GetSelectedSeat().Number} ({GetSelectedSeat().SeatTypeId})\n";

        return summary;
    }

    public static void ClearSelection()
    {
        _selectedMovie = null;
        _selectedSession = null;
        _selectedSeats.Clear();
        LoggerLogic.Instance.Log($"Reservation canceld | Summery:\n{GetConfirmationSummary()}");
    }

    public static void ClearMovie()
    {
        _selectedMovie = null;
        LoggerLogic.Instance.Log($"Cleared Movie");
    }

    public static void ClearSession()
    {
        _selectedSession = null;
        LoggerLogic.Instance.Log($"Cleared Session");
    }

    public static void ClearSeats()
    {
        _selectedSeats.Clear();
        LoggerLogic.Instance.Log($"Cleared Seat");
    }
}
