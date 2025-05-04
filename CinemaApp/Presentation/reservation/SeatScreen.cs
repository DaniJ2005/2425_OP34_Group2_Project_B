public class SeatScreen : IScreen
{
    public string ScreenName { get; set; }
    public SeatScreen() => ScreenName = "Seat Selection";

    public void Start()
    {
        ReservationLogic.ClearSeats();
        MovieSession moviesession = ReservationLogic.GetSelectedSession();

        SeatLogic.LoadSeats(moviesession);

        Screen(moviesession);
    }

    public void Screen(MovieSession moviesession)
    {
        ConsoleKey key;
        do
        {
            Console.Clear();

            // Display movie hall
            Console.WriteLine($"X: {SeatLogic.X}, Y: {SeatLogic.Y}\n");
            DisplaySeats();

            key = Console.ReadKey(true).Key;

            // Confirm Selection
            if (key == ConsoleKey.Enter)
            {
                SeatLogic.StoreSelection(moviesession);

                Console.Clear();
                Console.WriteLine("Selected Seats:");
                List<Seat> seats = ReservationLogic.GetSelectedSeats();
                foreach (var seat in seats)
                {
                    Console.WriteLine($"Row {seat.Row}, Col {seat.Col}");
                }
                Console.ReadKey();

                MenuLogic.NavigateTo(new ConfirmSelectionScreen());
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

                if (isCursor)
                {
                    Console.ForegroundColor = isSelected ? ConsoleColor.Green : ConsoleColor.White;
                    Console.Write("▣");
                    Console.ResetColor();
                }
                else if (isSelected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("■");
                    Console.ResetColor();
                }
                else if (isBooked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("▧");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("□");
                }

                
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

}