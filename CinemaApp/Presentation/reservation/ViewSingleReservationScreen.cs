public class ViewSingleReservationScreen : IScreen
{
    public string ScreenName { get; set; }
    private int _selectedIndex = 0;
    private string[] _buttons;
    private Reservation _reservation;
    private List<Ticket> _tickets;
    private List<ReservationFood> _foodItems;
    public ViewSingleReservationScreen() => ScreenName = "Food";

    public void Start()
    {
        _reservation = ReservationLogic.GetSelectedReservation();
        _tickets = ReservationLogic.GetTicketsByReservationId(_reservation.Id);
        _foodItems = ReservationLogic.GetFoodReservations(_reservation.Id);

        if (_reservation.Status == "Cancelled")
            _buttons = new[] { "Return" };
        else
            _buttons = new[] { "Cancel Reservation", "Return" };

        Screen();
    }

    public void Screen()
    {
        ConsoleKey key;

        do
        {
            General.ClearConsole();

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

            Console.WriteLine($"\nTotal Price: {_reservation.TotalPriceString}\n");

            for (int i = 0; i < _buttons.Length; i++)
            {
                string button = $"{_buttons[i]}";

                if (_selectedIndex == i)
                    Highlight(button);
                else Console.WriteLine(button);

            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (_selectedIndex > 0) _selectedIndex--;
                    break;
                case ConsoleKey.DownArrow:
                    if (_selectedIndex < _buttons.Length - 1) _selectedIndex++;
                    break;

                case ConsoleKey.Enter:
                    HandleSelection();
                    break;
            }


        } while (key != ConsoleKey.Escape);

        MenuLogic.NavigateToPrevious();
    }

    private void Highlight(string text)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    
    private void HandleSelection()
    {   
        switch (_buttons[_selectedIndex])
        {
            case "Cancel Reservation":
                // Cancel reservation
                ReservationLogic.CancelReservation(_reservation);

                Console.Clear();
                Console.WriteLine("Reservation Cancelled, Press any key to continue...");
                Console.ReadKey(true);

                MenuLogic.NavigateToPrevious();
                break;
            case "Return":
                MenuLogic.NavigateToPrevious();
                break;
        }
    }
}