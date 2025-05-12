using System.Reflection;

public class Table<T>
{
    private string[] _propertyNames;
    private string[] _headers;
    private List<T> _rows;
    private Dictionary<string, int> _columnWidths;
    private int _maxColWidth;
    private int _pageSize;
    private int _selectedIndex = 0;
    private int _pageIndex = 0;

    public Table(int maxColWidth = 30, int pageSize = 5)
    {
        _maxColWidth = maxColWidth;
        _pageSize = pageSize;
        _rows = new();

        PropertyInfo[] properties = typeof(T).GetProperties();
        _propertyNames = properties.Select(p => p.Name).ToArray();
        _headers = _propertyNames;
    }

    public void AddRow(T model) => _rows.Add(model);
    public void AddRows(List<T> models)
    {
        foreach (var model in models)
        {
            _rows.Add(model);
        }
    }
    public T? GetSelected()
    {
        if (_rows.Count == 0)
        return default;

        return _rows[_selectedIndex];
    }
        

    public void SetColumns(params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            if (!_propertyNames.Contains(propertyName))
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist in model '{typeof(T)}'.");
            }
        }

        _propertyNames = propertyNames;
    }

    public void Print(params string[] customHeaders)
    {
        if (_rows.Count == 0)
            return;

        SetCustomHeader(customHeaders);
        CalculateColumnWidths();

        PrintSeparator("top");
        PrintRow(_headers);
        PrintSeparator("middle");

        int start = _pageIndex * _pageSize;
        int end = Math.Min(start + _pageSize, _rows.Count);

        for (int i = start; i < end; i++)
        {
            var values = _propertyNames.Select(p =>
                Truncate(typeof(T).GetProperty(p)?.GetValue(_rows[i])?.ToString() ?? "")
            ).ToArray();
            if (i == _selectedIndex)
                Console.ForegroundColor = ConsoleColor.Yellow;
            
            PrintRow(values);
            Console.ResetColor();
        }

        PrintSeparator("bottom");

        // If there is more than 1 page, Show page index & page amount
        if (Math.Max(1, (int)Math.Ceiling((double)_rows.Count / _pageSize)) > 1)
            Console.WriteLine($"Page {_pageIndex + 1} of {Math.Max(1, (int)Math.Ceiling((double)_rows.Count / _pageSize))}");
    }

    public void MoveUp()
    {
        int start = _pageIndex * _pageSize;
        if (_selectedIndex > start)
            _selectedIndex--;
    }

    public void MoveDown()
    {
        int end = Math.Min((_pageIndex + 1) * _pageSize, _rows.Count);
        if (_selectedIndex < end - 1)
            _selectedIndex++;
    }

    public void NextPage()
    {
        int maxPages = (int)Math.Ceiling((double)_rows.Count / _pageSize);
        if (_pageIndex < maxPages - 1)
        {
            _pageIndex++;
            _selectedIndex = _pageIndex * _pageSize;
        }
    }

    public void PreviousPage()
    {
        if (_pageIndex > 0)
        {
            _pageIndex--;
            _selectedIndex = _pageIndex * _pageSize;
        }
    }

    private void SetCustomHeader(string[] customHeaders)
    {
        if (customHeaders.Length == 0)
        {
            _headers = _propertyNames;
            return;
        }

        if (_propertyNames.Length != customHeaders.Length)
        {
            throw new ArgumentException("Custom headers count does not match column count.");
        }

        _headers = customHeaders;
    }

    private void CalculateColumnWidths()
    {
        _columnWidths = new();

        for (int i = 0; i < _propertyNames.Length; i++)
        {
            string propName = _propertyNames[i];
            int maxLen = _headers[i].Length;

            foreach (var row in _rows)
            {
                string value = typeof(T).GetProperty(propName)?.GetValue(row)?.ToString() ?? "";
                if (value.Length > maxLen) maxLen = value.Length;
            }

            _columnWidths[propName] = Math.Min(maxLen, _maxColWidth);
        }
    }

    private void PrintRow(string[] values)
    {
        Console.Write("│");
        for (int i = 0; i < _propertyNames.Length; i++)
        {
            
            string prop = _propertyNames[i];
            int width = _columnWidths[prop];
            string value = Truncate(values[i]);
            Console.Write($" {value.PadRight(width)} │");
        }
        Console.WriteLine();
    }

    private string Truncate(string value) =>
        value.Length > _maxColWidth ? value.Substring(0, _maxColWidth - 3) + "..." : value;

    private void PrintSeparator(string type)
    {
        string left, middle, right, line;
        switch (type)
        {
            case "top":
                (left, middle, right, line) = ("┌", "┬", "┐", "─");
                break;
            case "middle":
                (left, middle, right, line) = ("├", "┼", "┤", "─");
                break;
            case "bottom":
                (left, middle, right, line) = ("└", "┴", "┘", "─");
                break;
            default:
                return;
        }

        Console.Write(left);
        for (int i = 0; i < _propertyNames.Length; i++)
        {
            string prop = _propertyNames[i];
            int width = _columnWidths[prop];
            Console.Write(new string(line[0], width + 2));
            if (i < _propertyNames.Length - 1)
                Console.Write(middle);
        }
        Console.WriteLine(right);
    }
}
