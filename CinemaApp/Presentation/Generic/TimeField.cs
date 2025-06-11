public class TimeField : FormField
{
    // Typical Dutch cinema start times (24h format)
    private readonly List<TimeSpan> _availableTimes = new()
    {
        new TimeSpan(6, 0, 0),   // 06:00
        new TimeSpan(6, 30, 0),  // 06:30
        new TimeSpan(7, 0, 0),   // 07:00
        new TimeSpan(7, 30, 0),  // 07:30
        new TimeSpan(8, 0, 0),   // 08:00
        new TimeSpan(8, 30, 0),  // 08:30
        new TimeSpan(9, 0, 0),   // 09:00
        new TimeSpan(9, 30, 0),  // 09:30
        new TimeSpan(10, 0, 0),  // 10:00
        new TimeSpan(10, 30, 0), // 10:30
        new TimeSpan(11, 0, 0),  // 11:00
        new TimeSpan(11, 30, 0), // 11:30
        new TimeSpan(12, 0, 0),  // 12:00
        new TimeSpan(12, 30, 0), // 12:30
        new TimeSpan(13, 0, 0),  // 13:00
        new TimeSpan(13, 30, 0), // 13:30
        new TimeSpan(14, 0, 0),  // 14:00
        new TimeSpan(14, 30, 0), // 14:30
        new TimeSpan(15, 0, 0),  // 15:00
        new TimeSpan(15, 30, 0), // 15:30
        new TimeSpan(16, 0, 0),  // 16:00
        new TimeSpan(16, 30, 0), // 16:30
        new TimeSpan(17, 0, 0),  // 17:00
        new TimeSpan(17, 30, 0), // 17:30
        new TimeSpan(18, 0, 0),  // 18:00
        new TimeSpan(18, 30, 0), // 18:30
        new TimeSpan(19, 0, 0),  // 19:00
        new TimeSpan(19, 30, 0), // 19:30
        new TimeSpan(20, 0, 0),  // 20:00
        new TimeSpan(20, 30, 0), // 20:30
        new TimeSpan(21, 0, 0),  // 21:00
        new TimeSpan(21, 30, 0), // 21:30
        new TimeSpan(22, 0, 0),  // 22:00
        new TimeSpan(22, 30, 0), // 22:30
        new TimeSpan(23, 0, 0),  // 23:00
        new TimeSpan(23, 30, 0)  // 23:30
    };

    private int _selectedIndex = 0;
    private int _page = 0;
    private const int PageSize = 6; // fits all times on one page, but support pages anyway

    public TimeField(string label) : base(label)
    {
        if (_availableTimes.Count > 0)
        {
            Value = FormatTime(_availableTimes[0]);
            _selectedIndex = 0;
            _page = 0;
        }
        else
        {
            Value = null;
            _selectedIndex = -1;
        }
    }

    private string FormatTime(TimeSpan time)
    {
        // Format as "HH:mm" in Dutch style (24h, leading zero)
        return time.ToString(@"hh\:mm");
    }

    public void RenderAndSelect(int left = 0, int top = 0)
    {
        if (_availableTimes.Count == 0)
        {
            Console.WriteLine("No available times.");
            Console.ReadKey(true);
            MenuLogic.NavigateToPrevious();
            return;
        }

        ConsoleKey key;
        int totalPages = (_availableTimes.Count + PageSize - 1) / PageSize;

        do
        {
            General.ClearConsole();

            Console.WriteLine($"{Label}:");
            Console.WriteLine("Use ↑↓ to navigate, ←→ to change page, [Enter] to select, [Esc] to cancel");
            Console.WriteLine();

            int start = _page * PageSize;
            int end = Math.Min(start + PageSize, _availableTimes.Count);

            for (int i = start; i < end; i++)
            {
                if (i == _selectedIndex)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ResetColor();

                Console.WriteLine($"  {FormatTime(_availableTimes[i])}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.Write($"Page {_page + 1}/{totalPages}".PadRight(30));

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                if (_selectedIndex > 0) _selectedIndex--;
                else _selectedIndex = _availableTimes.Count - 1;

                int newPage = _selectedIndex / PageSize;
                if (newPage != _page)
                    _page = newPage;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (_selectedIndex < _availableTimes.Count - 1) _selectedIndex++;
                else _selectedIndex = 0;

                int newPage = _selectedIndex / PageSize;
                if (newPage != _page)
                    _page = newPage;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                if (_page > 0) _page--;
                else _page = totalPages - 1;

                _selectedIndex = _page * PageSize;
                if (_selectedIndex >= _availableTimes.Count) _selectedIndex = _availableTimes.Count - 1;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (_page < totalPages - 1) _page++;
                else _page = 0;

                _selectedIndex = _page * PageSize;
                if (_selectedIndex >= _availableTimes.Count) _selectedIndex = _availableTimes.Count - 1;
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                return;
            }

        } while (key != ConsoleKey.Enter);

        if (key == ConsoleKey.Enter && _selectedIndex >= 0 && _selectedIndex < _availableTimes.Count)
        {
            Value = FormatTime(_availableTimes[_selectedIndex]);
        }
    }
}
