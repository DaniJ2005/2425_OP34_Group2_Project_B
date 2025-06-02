static class ReservationLogic
{
    private static Reservation _selectedReservation;
    private static Movie _selectedMovie;
    private static MovieSession _selectedSession;
    private static Dictionary<Seat, SeatPrice> _selectedSeats = [];
    private static Dictionary<Food, int> _selectedFoodItems = [];

    public static void SetSelectedReservation(Reservation reservation)
    {
        _selectedReservation = reservation;
    }

    public static void SetSelectedMovie(Movie movie)
    {
        _selectedMovie = movie;
    }
    public static void SetSelectedSession(MovieSession session)
    {
        _selectedSession = session;
    }
    public static void SetSelectedSeats(Dictionary<Seat, SeatPrice> seats)
    {
        _selectedSeats = new Dictionary<Seat, SeatPrice>(seats);
    }

    public static void SetSelectedFoodItems(Dictionary<Food, int> foodItems)
    {
        _selectedFoodItems = new Dictionary<Food, int>(foodItems);

        foreach (var kvp in foodItems)
        {
            LoggerLogic.Instance.Log($"Food item added: {kvp.Key.Id} - {kvp.Key.Name} x{kvp.Value}");
        }

    }
    public static Reservation GetSelectedReservation() => _selectedReservation;
    public static Movie GetSelectedMovie() => _selectedMovie;
    public static MovieSession GetSelectedSession() => _selectedSession;
    public static Dictionary<Seat, SeatPrice> GetSelectedSeats() => _selectedSeats;
    public static Dictionary<Food, int> GetSelectedFoodItems() => _selectedFoodItems;

    public static void CreateReservation(string email, double totalPrice)
    {
        ReservationAccess.CreateReservation(_selectedSession, _selectedSeats, _selectedFoodItems, email, UserLogic.CurrentUser, totalPrice);
        ClearSelection();
    }

    public static void CancelReservation(Reservation reservation)
    {
        ReservationAccess.CancelReservation(reservation);
    }

    public static List<Reservation> GetAllReservationsForCurrentUser()
    {
        User currentUser = UserLogic.CurrentUser;
        if (currentUser != null)
        {
            return ReservationAccess.GetReservationsByUserId(currentUser.Id);
        }
        return new List<Reservation>();
    }

    public static List<Ticket> GetTicketsByReservationId(int reservationId)
    {
        return TicketAccess.GetTicketsByReservationId(reservationId);
    }

    public static List<ReservationFood> GetFoodReservations(int reservationId)
    {
        return ReservationFoodAccess.GetFoodReservations(reservationId);
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
        _selectedFoodItems.Clear();
    }

    public static void ClearMovie()
    {
        _selectedMovie = null;
    }

    public static void ClearSession()
    {
        _selectedSession = null;
    }

    public static void ClearSeats()
    {
        _selectedSeats.Clear();
    }

    public static void ClearFood()
    {
        _selectedFoodItems.Clear();
    }

    public static void ClearSelectedReservation()
    {
        _selectedReservation = null;
    }
}
