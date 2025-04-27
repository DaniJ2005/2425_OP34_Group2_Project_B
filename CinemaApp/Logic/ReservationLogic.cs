static class ReservationLogic
{
    private static MovieModel _selectedMovie;
    private static MovieSessionModel _selectedSession;
    private static SeatModel _selectedSeat;

    public static void SetSelectedMovie(MovieModel movie)
    {
        LoggerLogic.Instance.Log($"Movie selected | ID: {movie.Id} | Title: {movie.Title}");
        _selectedMovie = movie;
    }
    public static void SetSelectedSession(MovieSessionModel session)
    {
        LoggerLogic.Instance.Log($"Session selected | ID: {session.Id} | MovieHallID: {session.MovieHallId} | Date: {session.Date}");
         _selectedSession = session;
    }
    public static void SetSelectedSeat(SeatModel seat)
    {
        LoggerLogic.Instance.Log($"Movie Seat | ID: {seat.Id} | Type: {seat.SeatTypeId}");
         _selectedSeat = seat;
    }

    public static MovieModel GetSelectedMovie() => _selectedMovie;
    public static MovieSessionModel GetSelectedSession() => _selectedSession;
    public static SeatModel GetSelectedSeat() => _selectedSeat;

    public static string GetConfirmationSummary()
    {
        if (_selectedMovie == null) //When session en seat are implemented add null check for 
            return "No reservation selected.";

        string summary = "";

        if (UserLogic.CurrentUser != null && !string.IsNullOrEmpty(UserLogic.CurrentUser.UserName))
            summary += $"User: {UserLogic.CurrentUser.UserName}\n";

        summary +=
            $"  - Movie: {GetSelectedMovie().Title}   ({GetSelectedMovie().Duration})\n" ;//+
            // $"  - Date: {GetSelectedSession().Date}\n" +
            // $"  - Time: {GetSelectedSession().StartTime}\n" +
            // $"  - Hall: {GetSelectedSession().MovieHallId}\n" +
            // $"  - Seat: Row {GetSelectedSeat().Row}, Seat {GetSelectedSeat().Number} ({GetSelectedSeat().SeatTypeId})\n";

        return summary;
    }

    public static void ClearSelection()
    {
        _selectedMovie = null;
        _selectedSession = null;
        _selectedSeat = null;
        LoggerLogic.Instance.Log($"Reservation canceld | Summery:\n{GetConfirmationSummary()}");
    }
}
