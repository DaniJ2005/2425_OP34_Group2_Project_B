static class RoomUI
{
    private static ConsoleColor RegularSeatColor = ConsoleColor.Blue;
    private static ConsoleColor PremiumSeatColor = ConsoleColor.DarkYellow;
    private static ConsoleColor VIPSeatColor = ConsoleColor.Red;
    private static ConsoleColor SelectedSeatColor = ConsoleColor.Green;
    private static ConsoleColor CursorColor = ConsoleColor.White;
    private static ConsoleColor NoSeatColor = ConsoleColor.Black;

    private static MovieHallModel _currentRoom;
    private static HashSet<(int row, int col)> _reservedSeats = new HashSet<(int row, int col)>();

    public static void SetCurrentRoom(MovieHallModel movieHall)
    {
        _currentRoom = movieHall;
        LoggerLogic.Instance.Log($"Selected room: {_currentRoom.Name} (ID: {_currentRoom.Id})");
    }

    public static void LoadReservedSeats(int sessionId)
    {
        _reservedSeats.Clear();
    }

    public static bool IsSeatReserved(int row, int col)
    {
        return _reservedSeats.Contains((row, col));
    }

    public static void DrawTheater(int cursorRow, int cursorCol, List<(int row, int col)> selectedSeats)
    {
        if (_currentRoom == null) return;

        int rows = _currentRoom.SeatLayout.GetLength(0);
        int cols = _currentRoom.SeatLayout.GetLength(1);

        Console.Write("   ");
        for (int col = 0; col < cols; col++)
        {
            Console.Write($"{col + 1,2} ");
        }
        Console.WriteLine();

        for (int row = rows - 1; row >= 0; row--)
        {
            Console.Write($"{row + 1,2} ");

            for (int col = 0; col < cols; col++)
            {
                int seatType = _currentRoom.SeatLayout[row, col];

                if (seatType == 0)
                {
                    Console.Write("   ");
                    continue;
                }

                ConsoleColor color;

                if (selectedSeats.Contains((row, col)))
                {
                    color = SelectedSeatColor;
                }
                else if (row == cursorRow && col == cursorCol)
                {
                    color = CursorColor;
                }
                else
                {
                    switch (seatType)
                    {
                        case 1: color = RegularSeatColor; break;
                        case 2: color = PremiumSeatColor; break;
                        case 3: color = VIPSeatColor; break;
                        default: color = NoSeatColor; break;
                    }
                }

                Console.BackgroundColor = color;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" ■ ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    public static void DrawLegend()
    {
        Console.WriteLine();

        Console.BackgroundColor = RegularSeatColor;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" ■ ");
        Console.ResetColor();
        Console.Write(" Regular ($10)  ");

        Console.BackgroundColor = PremiumSeatColor;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" ■ ");
        Console.ResetColor();
        Console.Write(" Premium ($15)  ");

        Console.BackgroundColor = VIPSeatColor;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" ■ ");
        Console.ResetColor();
        Console.Write(" VIP ($20)  ");

        Console.BackgroundColor = SelectedSeatColor;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" ■ ");
        Console.ResetColor();
        Console.WriteLine(" Selected");
    }

    public static void DrawScreen()
    {
        if (_currentRoom == null) return;

        Console.WriteLine();
        string screenLine = new string(' ', 14) + new string('▀', _currentRoom.SeatLayout.GetLength(1) * 3);
        Console.WriteLine(screenLine);
        Console.WriteLine(new string(' ', 20) + "SCREEN");
    }

    public static bool IsSeatValid(int row, int col)
    {
        if (_currentRoom == null)
            return false;

        if (row < 0 || row >= _currentRoom.SeatLayout.GetLength(0) || col < 0 || col >= _currentRoom.SeatLayout.GetLength(1))
            return false;

        return _currentRoom.SeatLayout[row, col] > 0;
    }

    public static int GetSeatTypeId(int row, int col)
    {
        if (!IsSeatValid(row, col))
            return 0;

        return _currentRoom.SeatLayout[row, col];
    }

    public static char GetRowLetter(int row)
    {
        return (char)('A' + row);
    }

    public static decimal GetPriceForSeatType(int seatType)
    {
        switch (seatType)
        {
            case 1: return 10.00m;
            case 2: return 15.00m;
            case 3: return 20.00m;
            default: return 0.00m;
        }
    }

    public static string GetSeatTypeName(int seatType)
    {
        switch (seatType)
        {
            case 1: return "Regular";
            case 2: return "Premium";
            case 3: return "VIP";
            default: return "Unknown";
        }
    }

    public static (int, int) FindInitialCursorPosition()
    {
        if (_currentRoom == null)
            return (0, 0);

        int startRow = _currentRoom.SeatLayout.GetLength(0) / 2;
        int startCol = _currentRoom.SeatLayout.GetLength(1) / 2;

        if (!IsSeatValid(startRow, startCol))
        {
            for (int row = 0; row < _currentRoom.SeatLayout.GetLength(0); row++)
            {
                for (int col = 0; col < _currentRoom.SeatLayout.GetLength(1); col++)
                {
                    if (IsSeatValid(row, col))
                    {
                        return (row, col);
                    }
                }
            }
        }

        return (startRow, startCol);
    }
}