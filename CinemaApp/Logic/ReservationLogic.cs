static class ReservationLogic
{
    private static Movie _selectedMovie;
    private static MovieSession _selectedSession;
    private static Dictionary<Seat, SeatPrice> _selectedSeats = [];
    private static List<Food> _selectedFoodItems = [];

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
    public static void SetSelectedSeats(Dictionary<Seat, SeatPrice> seats)
    {
        foreach (var seat in seats)
        {
            LoggerLogic.Instance.Log($"Movie Seat | ID: {seat.Key.Id} | Type: {seat.Key.SeatTypeId} | Price: {seat.Value.Price}");
        }
        _selectedSeats = new Dictionary<Seat, SeatPrice>(seats);
    }

    public static void SetSelectedFoodItems(List<Food> foodItems)
    {
        foreach (var foodItem in foodItems)
        {
            LoggerLogic.Instance.Log($"Food Item added to reservation | ID: {foodItem.Id} | Type: {foodItem.Name}");
        }
        _selectedFoodItems = new List<Food>(foodItems);
    }

    public static Movie GetSelectedMovie() => _selectedMovie;
    public static MovieSession GetSelectedSession() => _selectedSession;
    public static Dictionary<Seat, SeatPrice> GetSelectedSeats() => _selectedSeats;
    public static List<Food> GetSelectedFoodItems() => _selectedFoodItems;

    public static void CreateReservation(string email)
    {
        ReservationAccess.CreateReservation(_selectedSession, _selectedSeats, _selectedFoodItems, email);
    }
        

    public static string GetConfirmationSummary()
    {
        if (_selectedMovie == null) //When session en seat are implemented add null check for 
            return "No reservation selected.";

        string summary = "";

        summary +=
            $"  - Movie: {GetSelectedMovie().Title}   ({GetSelectedMovie().Duration})\n" +
            $"  - Date: {GetSelectedSession().Date}\n" +
            $"  - Time: {GetSelectedSession().StartTime}\n" +
            $"  - Hall: {GetSelectedSession().MovieHallId}\n";

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
