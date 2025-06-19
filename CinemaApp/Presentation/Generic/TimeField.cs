using System.Globalization;

public class TimeField : FormField
{
    private readonly List<TimeSpan> _availableTimes = new();
    private int _selectedIndex = 0;
    private int _page = 0;
    private const int PageSize = 12;

    public TimeField(string label, List<MovieSession> existingTimes, TimeSpan movieDuration) : base(label)
    {
        // Build list of blocked ranges from existing sessions
        var existingRanges = existingTimes
            .Select(s => (Start: s.StartDateTime, End: s.EndDateTime))
            .ToList();

        // Time range current date: from 12:00 to 23:00
        DateTime baseDate = existingTimes.FirstOrDefault()?.StartDateTime.Date ?? DateTime.Today;
        TimeSpan firstSlot = new TimeSpan(12, 0, 0);
        TimeSpan lastSlot = new TimeSpan(23, 0, 0);

        for (TimeSpan ts = firstSlot; ts <= lastSlot; ts = ts.Add(TimeSpan.FromMinutes(10)))
        {
            DateTime candidateStart = baseDate + ts;
            DateTime candidateEnd = candidateStart + movieDuration;

            // Check for any overlap with existing sessions
            bool overlaps = existingRanges.Any(r =>
                candidateStart < r.End && candidateEnd > r.Start
            );

            if (!overlaps)
            {
                _availableTimes.Add(ts);
            }
        }

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

    public void RenderAndSelect()
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
