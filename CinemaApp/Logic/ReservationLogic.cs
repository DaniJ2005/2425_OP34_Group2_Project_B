static class ReservationLogic
{
    private static MovieModel _selectedMovie;
    private static MovieSessionModel _selectedSession;
    private static SeatModel _selectedSeat;

    public static void SetSelectedMovie(MovieModel movieId) => _selectedMovie = movieId;
    public static void SetSelectedSession(MovieSessionModel sessionId) => _selectedSession = sessionId;
    public static void SetSelectedSeat(SeatModel seatId) => _selectedSeat = seatId;

    public static MovieModel GetSelectedMovie() => _selectedMovie;
    public static MovieSessionModel GetSelectedSession() => _selectedSession;
    public static SeatModel GetSelectedSeat() => _selectedSeat;

    public static string GetConfirmationSummary(long userId)
        {
            if (_selectedSession == null || _selectedSeat == null)
                return "No reservation selected.";

            return
                $"User ID: {userId}\n" +
                $"  - Movie: {_selectedMovie.Title}\n" +
                $"  - Time: {_selectedSession.StartTime}\n" +
                $"  - Hall: {_selectedSession.MovieHallId}\n" +
                $"  - Seat: Row {_selectedSeat.Row}, Seat {_selectedSeat.Number} ({_selectedSeat.SeatTypeId})\n";
        }
}
