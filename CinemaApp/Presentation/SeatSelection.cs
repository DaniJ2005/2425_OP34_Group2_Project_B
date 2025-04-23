
static class SeatSelection
{
    private const int MaxSeats = 8;
    
    private static int cursorRow;
    private static int cursorCol;
    private static List<(int row, int col)> selectedSeats = new List<(int row, int col)>();
    
    public static void Start()
    {
        if (ReservationLogic.GetSelectedMovie() == null)
        {
            Console.WriteLine("Please select a movie first.");
            Console.ReadKey(true);
            MovieSelection.Start();
            return;
        }
        
        // Get the movie hall object instead of just the ID
        var selectedMovie = ReservationLogic.GetSelectedMovie();
        var movieHall = MovieHallAccess.GetHallById(selectedMovie.MovieHallId);
        
        if (movieHall == null)
        {
            Console.WriteLine("Error: Could not find the movie hall.");
            Console.ReadKey(true);
            Menu.Start();
            return;
        }
        
        RoomUI.SetCurrentRoom(movieHall);
        selectedSeats.Clear();
        (cursorRow, cursorCol) = RoomUI.FindInitialCursorPosition();
        
        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.WriteLine($"Movie: {selectedMovie.Title}");
            Console.WriteLine($"Duration: {selectedMovie.Duration}\n");
            Console.WriteLine("Select seats (↑↓←→: Move | Space: Select | Enter: Confirm | Esc: Cancel)");
            Console.WriteLine($"Selected: {selectedSeats.Count}/{MaxSeats} seats\n");
            
            RoomUI.DrawTheater(cursorRow, cursorCol, selectedSeats);
            RoomUI.DrawScreen();
            RoomUI.DrawLegend();
            
            DrawSelectedSeats();
            
            key = Console.ReadKey(true).Key;
            HandleNavigation(key);
            HandleSelection(key);
            
        } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
        
        if (key == ConsoleKey.Escape)
        {
            LoggerLogic.Instance.Log("User canceled seat selection");
            MovieSelection.Start();
            return;
        }
        
        if (key == ConsoleKey.Enter && selectedSeats.Count > 0)
        {
            SaveSelections();
            ConfirmSelection.Start(); 
        }
        else if (key == ConsoleKey.Enter)
        {
            Console.WriteLine("\nPlease select at least one seat before continuing.");
            Console.ReadKey(true);
            Start(); 
        }
    }
    
    private static void DrawSelectedSeats()
    {
        if (selectedSeats.Count > 0)
        {
            Console.WriteLine("\nSelected seats:");
            decimal totalPrice = 0.00m;
            
            foreach (var seat in selectedSeats)
            {
                int seatType = RoomUI.GetSeatTypeId(seat.row, seat.col);
                string seatTypeName = RoomUI.GetSeatTypeName(seatType);
                decimal price = RoomUI.GetPriceForSeatType(seatType);
            
                Console.WriteLine($"- Row {seat.row + 1}, Seat {seat.col + 1} - {seatTypeName} (${price:0.00})");
                totalPrice += price;
            }
            
            Console.WriteLine($"\nTotal price: ${totalPrice:0.00}");
        }
    }
    
    private static void HandleNavigation(ConsoleKey key)
    {
        int newRow = cursorRow;
        int newCol = cursorCol;
        
        switch (key)
        {
            case ConsoleKey.UpArrow:
                newRow++;  
                break;
            case ConsoleKey.DownArrow:
                newRow--; 
                break;
            case ConsoleKey.LeftArrow:
                newCol--;
                break;
            case ConsoleKey.RightArrow:
                newCol++;
                break;
        }
        
        if (RoomUI.IsSeatValid(newRow, newCol))
        {
            cursorRow = newRow;
            cursorCol = newCol;
        }
    }
    
    private static void HandleSelection(ConsoleKey key)
    {
        if (key == ConsoleKey.Spacebar)
        {
            if (RoomUI.IsSeatValid(cursorRow, cursorCol))
            {
                var currentSeat = (row: cursorRow, col: cursorCol);
                
                int index = selectedSeats.FindIndex(s => s.row == currentSeat.row && s.col == currentSeat.col);
                if (index >= 0)
                {
                    selectedSeats.RemoveAt(index);
                    LoggerLogic.Instance.Log($"Seat unselected | Row {cursorRow + 1}, Seat {cursorCol + 1}");
                }
                else if (selectedSeats.Count < MaxSeats)
                {
                    selectedSeats.Add(currentSeat);
                    LoggerLogic.Instance.Log($"Seat selected | Row {cursorRow + 1}, Seat {cursorCol + 1}");
                }
            }
        }
    }
    
    private static void SaveSelections()
    {
        ReservationLogic.ClearSelection();
        
        ReservationLogic.SetSelectedMovie(ReservationLogic.GetSelectedMovie());
        
        // Save all selected seats
        foreach (var seat in selectedSeats)
        {
            SeatModel seatModel = new SeatModel
            {
                Row = RoomUI.GetRowLetter(seat.row),
                Number = seat.col + 1,
                SeatTypeId = RoomUI.GetSeatTypeId(seat.row, seat.col),
                MovieHallId = ReservationLogic.GetSelectedMovie().MovieHallId
            };
            
            LoggerLogic.Instance.Log($"Saved seat to reservation: Row {seat.row + 1}, Seat {seat.col + 1}");
        }
        
        LoggerLogic.Instance.Log($"Selected and saved {selectedSeats.Count} seats");
    }
}