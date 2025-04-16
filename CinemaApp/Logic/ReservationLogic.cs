static class ReservationLogic
{
    public static int SelectedMovieId;
    public static int SelectedSessionId;
    public static int SelectedSeatId;

    public static void SetSelectedMovie(int movieId) => SelectedMovieId = movieId;
    public static void SetSelectedSession(int sessionId) => SelectedSessionId = sessionId;
    public static void SetSelectedSeat(int seatId) => SelectedSeatId = seatId;
}
