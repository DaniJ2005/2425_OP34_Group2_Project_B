public class OverviewScreen : IScreen
{
    public string ScreenName { get; set; }
    private int _selectedIndex = 0;
    private List<KeyValuePair<Seat, SeatPrice>> _seats;
    private string[] _buttons;
    private List<KeyValuePair<Food, int>> _selectedFoods = [];
    private User _currentUser;
    public OverviewScreen() => ScreenName = "Reservation Overview";

    public void Start()
    {
        _seats = ReservationLogic.GetSelectedSeats().ToList();
        _selectedFoods = ReservationLogic.GetSelectedFoodItems().ToList();
        _currentUser = UserLogic.CurrentUser;
        

        if (_currentUser != null)
        {
            // If logged in
            _buttons = new[] { "Add Food", "Confirm Reservation" };
        }
        else
        {
            // If not logged in
            // _buttons = new[] { "Add Food", "Log In", "Create Account", "Continue as Guest" };
            _buttons = new[] { "Add Food", "Log In", "Continue as Guest" };
        }

        Screen();
    }

    private void Screen()
    {
        ConsoleKey key;

        do
        {
            string confirmationSummary = ReservationLogic.GetConfirmationSummary();

            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.White);
            Console.WriteLine();

            if (_currentUser != null)
            {
                Console.WriteLine($"User: {_currentUser.Email}\n");
            }
            else
            {
                Console.WriteLine("No logged in user\n");
            }

            Console.WriteLine($"{confirmationSummary}");

            double totalPrice = 0;
            
            // Display Selected food items
            foreach (var selectedFoodKvp in _selectedFoods)
            {
                int quantity = selectedFoodKvp.Value;
                Food food = selectedFoodKvp.Key;

                totalPrice += food.Price * quantity;

                Console.WriteLine($" - {quantity}x {food.Name} - €{food.Price * quantity:F2}");
            }

            Console.WriteLine();

            // Display Selected seats
            for (int i = 0; i < _seats.Count; i++)
            {
                var seatText = $" - Seat | Row: {_seats[i].Key.Row} | Col: {_seats[i].Key.Col} | Price: €{_seats[i].Value.Price}";
                totalPrice += _seats[i].Value.Price;
                if (_selectedIndex == i)
                    Highlight(seatText);
                else
                    Console.WriteLine(seatText);
            }

            Console.WriteLine($"\nTotal: €{totalPrice:F2}\n");

            for (int i = 0; i < _buttons.Length; i++)
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
                    if (_selectedIndex < _seats.Count + _buttons.Length - 1) _selectedIndex++;
                    break;

                case ConsoleKey.Enter:
                    HandleSelection(totalPrice);
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

    private void HandleSelection(double totalPrice)
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
                    HandleConfirmReservation(_currentUser.Email, totalPrice);
                    break;
                case "Continue as Guest":
                    HandleGuestConfirm(totalPrice);
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
    }

    private void HandleConfirmReservation(string email, double totalPrice)
    {
        General.ClearConsole();
        Console.WriteLine("Reservation Confirmed!\n");

        string confirmationSummary = ReservationLogic.GetConfirmationSummary();

        Console.WriteLine($"Email: {email}\n");

        Console.WriteLine($"{confirmationSummary}");
        
        // Display Selected food items
        foreach (var selectedFoodKvp in _selectedFoods)
        {
            int quantity = selectedFoodKvp.Value;
            Food food = selectedFoodKvp.Key;

            Console.WriteLine($" - {quantity}x {food.Name} - €{food.Price * quantity:F2}");
        }

        Console.WriteLine();

        // Display Selected seats
        for (int i = 0; i < _seats.Count; i++)
        {
            var seatText = $" - Seat | Row: {_seats[i].Key.Row} | Col: {_seats[i].Key.Col} | Price: €{_seats[i].Value.Price}";
            if (_selectedIndex == i)
                Highlight(seatText);
            else
                Console.WriteLine(seatText);
        }

        Console.WriteLine($"\nTotal: €{totalPrice:F2}\n");
        Console.WriteLine("Press any key to return...");
        ReservationLogic.CreateReservation(email, totalPrice);
        Console.ReadKey(true);
        MenuLogic.NavigateTo(new HomeScreen(), clearStack: true);
    }

    private void HandleGuestConfirm(double totalPrice)
    {
        General.ClearConsole();
        Console.WriteLine("Enter email for the reservation.");

        string email = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Reservation Confirmed!\n");

        string confirmationSummary = ReservationLogic.GetConfirmationSummary();

        Console.WriteLine($"Email: {email}\n");

        Console.WriteLine($"{confirmationSummary}");
        
        // Display Selected food items
        foreach (var selectedFoodKvp in _selectedFoods)
        {
            int quantity = selectedFoodKvp.Value;
            Food food = selectedFoodKvp.Key;

            Console.WriteLine($" - {quantity}x {food.Name} - €{food.Price * quantity:F2}");
        }

        Console.WriteLine();

        // Display Selected seats
        for (int i = 0; i < _seats.Count; i++)
        {
            var seatText = $" - Seat | Row: {_seats[i].Key.Row} | Col: {_seats[i].Key.Col} | Price: €{_seats[i].Value.Price}";
            if (_selectedIndex == i)
                Highlight(seatText);
            else
                Console.WriteLine(seatText);
        }

        Console.WriteLine($"\nTotal: €{totalPrice:F2}\n");

        Console.WriteLine("Press any key to return...");
        ReservationLogic.CreateReservation(email, totalPrice);
        Console.ReadKey(true);
        MenuLogic.NavigateTo(new HomeScreen(), clearStack: true);
    }

    private void HandleAddFood() => MenuLogic.NavigateTo(new FoodScreen());
}
