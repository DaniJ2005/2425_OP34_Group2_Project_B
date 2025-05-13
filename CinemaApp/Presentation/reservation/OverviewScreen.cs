public class OverviewScreen : IScreen
{
    public string ScreenName { get; set; }
    private int _selectedIndex = 0;
    private List<KeyValuePair<Seat, SeatPrice>> _seats;
    private List<string> _buttons;

    public OverviewScreen() => ScreenName = "Confirm Reservation";

    public void Start()
    {
        _seats = ReservationLogic.GetSelectedSeats().ToList();
        if (UserLogic.CurrentUser != null)
        {
            // If logged in
            _buttons = new(){ "Add Food", "Confirm Reservation" };
        }
        else
        {
            // If not logged in
            // _buttons = new(){ "Add Food", "Log In", "Create Account", "Continue as Guest"};
            _buttons = new(){ "Add Food", "Continue as Guest"};
        }
        Screen();
    }

    private void Screen()
    {
        ConsoleKey key;
        int topPosition = Console.CursorTop;

        do
        {
            string confirmationSummary = ReservationLogic.GetConfirmationSummary();

            General.ClearConsole(topPosition);
            Console.WriteLine("Reservation:\n");
            Console.WriteLine($"{confirmationSummary}");

            for (int i = 0; i < _seats.Count; i++)
            {
                var seatText = $" - Seat | Row: {_seats[i].Key.Row} | Col: {_seats[i].Key.Col} | Price: €{_seats[i].Value.Price}";
                if (_selectedIndex == i)
                    Highlight(seatText);
                else
                    Console.WriteLine(seatText);
            }

            Console.WriteLine();

            for (int i = 0; i < _buttons.Count; i++)
            {
                int buttonIndex = _seats.Count + i;
                if (_selectedIndex == buttonIndex)
                    Highlight(_buttons[i]);
                else
                    Console.WriteLine(_buttons[i]);
            }

            

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (_selectedIndex > 0) _selectedIndex--;
                    break;

                case ConsoleKey.DownArrow:
                    if (_selectedIndex < _seats.Count + _buttons.Count - 1) _selectedIndex++;
                    break;

                case ConsoleKey.Enter:
                    HandleSelection();
                    break;

                case ConsoleKey.Escape:
                    MenuLogic.NavigateToPrevious();
                    return;
            }

        } while (true);
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
        // If selected a seat
        if (_selectedIndex < _seats.Count)
        {
            var selectedKvp = _seats[_selectedIndex];
            HandleSeatAction(selectedKvp);
        }
        else
        {
            var buttonIndex = _selectedIndex - _seats.Count;
            switch (_buttons[buttonIndex])
            {
                case "Confirm Reservation":
                    // If user logged in
                    HandleConfirmReservation(UserLogic.CurrentUser.Email);
                    break;
                case "Continue as Guest":
                    HandleGuestConfirm();
                    break;
                case "Log In":
                    MenuLogic.NavigateTo(new Login());
                    break;
                case "Create Account":
                    MenuLogic.NavigateTo(new Register());
                    break;
                case "Add Food":
                    HandleAddFood();
                    break;
            }
        }
    }

    private void HandleSeatAction(KeyValuePair<Seat, SeatPrice> seatKvp)
    {
        Console.WriteLine($"\n[Seat Selected] {seatKvp.Key.Id} - {seatKvp.Value.Price}");
        Console.ReadKey(true);
    }

    private void HandleConfirmReservation(string email)
    {
        ReservationLogic.CreateReservation(email);
        MenuLogic.NavigateTo(new HomeScreen(), clearStack: true);
    }

    private void HandleGuestConfirm()
    {
        Console.Clear();
        Console.WriteLine("Enter email for the reservation.");

        string email = Console.ReadLine();
        ReservationLogic.CreateReservation(email);

        Console.WriteLine("Reservation Confirmed!");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
        MenuLogic.NavigateTo(new HomeScreen(), clearStack: true);
    }

    private void HandleAddFood() => MenuLogic.NavigateTo(new FoodScreen());
}
