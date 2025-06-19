public class DateField : FormField
{
    private readonly List<DateTime> _availableDates;
    private readonly int _movieHallId;
    private readonly string _startTime;
    private readonly List<MovieSession> _existingSessions;

    private int _selectedIndex = 0;
    private int _page = 0;
    private const int PageSize = 8; // max 7 days (1 week)

    public DateField(string label, int movieHallId, List<MovieSession> existingSessions) : base(label)
    {
        _movieHallId = movieHallId;
        _existingSessions = existingSessions;

        // Prepare available dates: from today up to 7 days ahead, excluding duplicates
        _availableDates = new List<DateTime>();
        DateTime today = DateTime.Today;

        for (int i = 0; i <= 7; i++)
        {
            DateTime date = today.AddDays(i);

            _availableDates.Add(date);
            
        }

        if (_availableDates.Count > 0)
        {
            Value = _availableDates[0].ToString("yyyy-MM-dd");
            _selectedIndex = 0;
            _page = 0;
        }
        else
        {
            // No valid dates
            Value = null;
            _selectedIndex = -1;
        }
    }

    public void RenderAndSelect()
    {
        if (_availableDates.Count == 0)
        {
            Console.WriteLine("No available dates for the selected MovieHall and time.");
            Console.ReadKey(true);
            MenuLogic.NavigateToPrevious();
            return;
        }

        ConsoleKey key;
        int totalPages = (_availableDates.Count + PageSize - 1) / PageSize;

        do
        {
            General.ClearConsole();

            Console.WriteLine($"{Label}:");
            Console.WriteLine("Use ↑↓ to navigate, ←→ to change page, [Enter] to select, [Esc] to cancel");
            Console.WriteLine();

            int start = _page * PageSize;
            int end = Math.Min(start + PageSize, _availableDates.Count);

            for (int i = start; i < end; i++)
            {
                if (i == _selectedIndex)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ResetColor();

                Console.WriteLine($"  {_availableDates[i]:yyyy-MM-dd} ({_availableDates[i]:ddd})");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.Write($"Page {_page + 1}/{totalPages}".PadRight(30));

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                if (_selectedIndex > 0) _selectedIndex--;
                else _selectedIndex = _availableDates.Count - 1;

                int newPage = _selectedIndex / PageSize;
                if (newPage != _page)
                    _page = newPage;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (_selectedIndex < _availableDates.Count - 1) _selectedIndex++;
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
                if (_selectedIndex >= _availableDates.Count) _selectedIndex = _availableDates.Count - 1;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (_page < totalPages - 1) _page++;
                else _page = 0;

                _selectedIndex = _page * PageSize;
                if (_selectedIndex >= _availableDates.Count) _selectedIndex = _availableDates.Count - 1;
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                return;
            }

        } while (key != ConsoleKey.Enter);

        if (key == ConsoleKey.Enter && _selectedIndex >= 0 && _selectedIndex < _availableDates.Count)
        {
            Value = _availableDates[_selectedIndex].ToString("yyyy-MM-dd");
        }
    }
}
