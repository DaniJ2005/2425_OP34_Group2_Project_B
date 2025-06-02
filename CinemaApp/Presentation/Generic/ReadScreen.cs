public class ReadScreen<T>
{
    //this is for admins that dont have the rights to add, update or delete items
    private readonly Func<List<T>> _loadItems;
    private string[] _headers;

    public ReadScreen(Func<List<T>> loadItems, string[] headers = null)
    {
        _loadItems = loadItems;
        _headers = headers ?? Array.Empty<string>();
    }

    public void Start()
    {
        var items = _loadItems();

        Table<T> table = new(maxColWidth: 40, pageSize: 10);
        table.SetColumns(_headers);
        table.AddRows(items);

        ConsoleKey key;
        do
        {
            General.ClearConsole();
            Console.WriteLine("==== Viewing Items ====\n");
            Console.WriteLine("[↑][↓] to navigate, [←][→] to change page, [ESC] to return.\n");
            table.Print(_headers);

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    table.MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    table.MoveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    table.PreviousPage();
                    break;
                case ConsoleKey.RightArrow:
                    table.NextPage();
                    break;
                case ConsoleKey.Escape:
                    MenuLogic.NavigateToPrevious();
                    return;
            }

        } while (true);
    }
}
