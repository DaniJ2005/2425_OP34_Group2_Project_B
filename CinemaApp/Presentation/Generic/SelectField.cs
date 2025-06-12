public class SelectField : FormField
{
    private readonly Dictionary<string, string> _options;
    private List<string> _keys;
    private int _selectedIndex = 0;
    private int _page = 0;
    private const int PageSize = 10;

    public SelectField(string label, Dictionary<string, string> options) : base(label)
    {
        _options = options;
        _keys = _options.Keys.ToList();

        if (_keys.Count > 0)
        {
            Value = _keys[0];
            _selectedIndex = 0;
            _page = 0;
        }
    }

    public void RenderAndSelect(int left = 0, int top = 0)
    {
        ConsoleKey key;

        if (!string.IsNullOrEmpty(Value))
        {
            int idx = _keys.IndexOf(Value);
            if (idx >= 0)
            {
                _selectedIndex = idx;
                _page = _selectedIndex / PageSize;
            }
        }

        int totalPages = (_keys.Count + PageSize - 1) / PageSize;

        do
        {
            General.ClearConsole();

            Console.WriteLine($"{Label}:");
            Console.WriteLine("Use ↑↓ to navigate, ←→ to change page, [Enter] to select, [Esc] to cancel");
            Console.WriteLine();

            int start = _page * PageSize;
            int end = Math.Min(start + PageSize, _keys.Count);

            // Render current page options
            for (int i = start; i < end; i++)
            {
                if (i == _selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ResetColor();
                }

                Console.WriteLine($"  {_options[_keys[i]]}");
                Console.ResetColor();
            }

            // Show page info below options
            Console.WriteLine();
            Console.Write($"Page {_page + 1}/{totalPages}".PadRight(30));

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                if (_selectedIndex > 0) _selectedIndex--;
                else _selectedIndex = _keys.Count - 1;

                int newPage = _selectedIndex / PageSize;
                if (newPage != _page)
                    _page = newPage;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (_selectedIndex < _keys.Count - 1) _selectedIndex++;
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
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (_page < totalPages - 1) _page++;
                else _page = 0;

                _selectedIndex = _page * PageSize;
            }
            else if (key == ConsoleKey.Escape)
                MenuLogic.NavigateTo(new SessionManagementScreen());

        } while (key != ConsoleKey.Enter);

        if (key == ConsoleKey.Enter && _selectedIndex >= 0 && _selectedIndex < _keys.Count)
        {
            Value = _keys[_selectedIndex];
        }
    }

}
