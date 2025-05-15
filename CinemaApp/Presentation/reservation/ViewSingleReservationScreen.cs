public class ViewSingleReservationScreen : IScreen
{
    public string ScreenName { get; set; }
    private Reservation _reservation;
    private List<Ticket> _tickets;
    private List<ReservationFood> _foodItems;
    public ViewSingleReservationScreen() => ScreenName = "Food";

    public void Start()
    {
        _reservation = ReservationLogic.GetSelectedReservation();
        _tickets = ReservationLogic.GetTicketsByReservationId(_reservation.Id);
        _foodItems = ReservationLogic.GetFoodReservations(_reservation.Id);
        Screen();
    }

    public void Screen()
    {
        ConsoleKey key;

        do
        {
            Console.Clear();

            Console.WriteLine("Your Reservation:\n");

            Console.WriteLine($"Movie: {_reservation.MovieTitle}");
            Console.WriteLine($"Date: {_reservation.Date} - {_reservation.StartTime}");
            Console.WriteLine($"Movie Hall: {_reservation.MovieHall}\n");

            Console.WriteLine($"Reservation Created: {_reservation.CreatedAt}\n");

            Console.WriteLine("Seat Tickets:\n");

            foreach (var t in _tickets)
            {
                string promo = t.Promo != "none" ? $"({t.Promo})" : "";
                Console.WriteLine($" - Ticket: [{t.SeatType}] - Seat: [R{t.Row}#{t.Col}] - €{t.Price} {promo}");
            }

            Console.WriteLine("\nFood & Drinks:\n");

            foreach (var f in _foodItems)
            {
                string priceString = $"€{f.Price * f.Quantity:F2}";
                Console.WriteLine($" - {f.Quantity}x {f.Name} - {priceString}");
            }

            Console.WriteLine($"\nTotal Price: {_reservation.TotalPriceString}");
        
            key = Console.ReadKey(true).Key;

            

        } while (key != ConsoleKey.Escape);

        MenuLogic.NavigateToPrevious();
    }
}