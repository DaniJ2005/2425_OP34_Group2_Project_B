public class SeatScreen : IScreen
{
    public string ScreenName { get; set; }
    private string error = "";
    private List<SeatPrice> _seatPrices;
    public SeatScreen() => ScreenName = "Seat Selection";

    public void Start()
    {
        error = "";
        ReservationLogic.ClearSeats();
        MovieSession moviesession = ReservationLogic.GetSelectedSession();

        _seatPrices = SeatLogic.GetSeatPrices();

        SeatLogic.LoadSeats(moviesession);

        Screen(moviesession);
    }

    public void Screen(MovieSession moviesession)
    {
        ConsoleKey key;

        do
        {
            General.ClearConsole();  
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.White);
            Console.WriteLine();
            Console.WriteLine($"Please Select your seats. (Max {SeatLogic.SeatSelectionLimit})\n");
            Console.WriteLine("[←][↑][→][↓] to navigate\n[SPACE] to select a seat,\n[ENTER] to confirm your selection.\n");

            if(error != "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error! {error}\n");
                Console.ResetColor();
            }

            // Display movie hall
            DisplaySeats();

            // Display legend
            DisplayLegend();

            key = Console.ReadKey(true).Key;

            // Confirm Selection
            if (key == ConsoleKey.Enter)
            {
                if (SeatLogic.SelectedSeatIds.Count == 0)
                {
                    error = "You must select one or more seats.";
                    continue;
                }
                
                SeatLogic.StoreSelection(moviesession);
                MenuLogic.NavigateTo(new OverviewScreen());
            }

            // Select Seat
            if (key == ConsoleKey.Spacebar)
            {
                SeatLogic.ToggleSeatSelection();
            }
            
            // Move Up, Down, Left, Right
            switch(key)
            {
                case ConsoleKey.UpArrow: SeatLogic.MoveUp(); break;
                case ConsoleKey.DownArrow: SeatLogic.MoveDown(); break;
                case ConsoleKey.RightArrow: SeatLogic.MoveRight(); break;
                case ConsoleKey.LeftArrow: SeatLogic.MoveLeft(); break;
            }
        }
        while (key != ConsoleKey.Escape);

        MenuLogic.NavigateToPrevious();
    }

    public void DisplaySeats()
    {
        for (int row = 0; row < SeatLogic.SeatGrid.GetLength(0); row++)
        {
            Console.Write($"Row {row + 1,2}  ");
            for (int col = 0; col < SeatLogic.SeatGrid.GetLength(1); col++)
            {
                var seat = SeatLogic.SeatGrid[row, col];

                if (seat == null)
                {
                    Console.Write("- ");
                    continue;
                }

                bool isCursor = SeatLogic.Y == row && SeatLogic.X == col;
                bool isSelected = SeatLogic.SelectedSeatIds.Contains(seat.Id);
                bool isBooked = SeatLogic.BookedSeatIds.Contains(seat.Id);

                if (isBooked)
                {
                    General.PrintColoredString("▧", "Red");
                } else if (isCursor)
                {
                    General.PrintColoredString("▣", seat.Color);
                } else if (isSelected)
                {
                    General.PrintColoredString("■", seat.Color);
                } else
                {
                    General.PrintColoredString("□", seat.Color);
                }
                
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    private void DisplayLegend()
    {
        Console.WriteLine("\nLegend:");
        foreach (var seatPrice in _seatPrices)
        {
            // Print the colored square
            General.PrintColoredString("□", seatPrice.Color);
            Console.WriteLine($" - {seatPrice.Type} (${seatPrice.Price:0.00})");
        }

        // Also print the fixed icons (for booked and cursor)
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("▧");
        Console.ResetColor();
        Console.WriteLine(" - Booked");
    }

    public void icons()
    {
        Console.Write("▣");
        Console.Write("■");
        Console.Write("▧");
        Console.Write("□");
    }

}