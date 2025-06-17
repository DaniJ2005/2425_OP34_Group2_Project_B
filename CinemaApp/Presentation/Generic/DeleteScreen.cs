public class DeleteScreen<T>
{
    private readonly Func<List<T>> _loadItems;
    private readonly Func<T, bool> _deleteItem;
    private string[] _headers;


    public DeleteScreen(Func<List<T>> loadItems, Func<T, bool> deleteItem, string[] headers = null)
    {
        _loadItems = loadItems;
        _deleteItem = deleteItem;
        _headers = headers ?? Array.Empty<string>();
    }

    public void Start()
    {
        var items = _loadItems();
        var table = new Table<T>(30, 10);
        table.SetColumns(_headers);
        table.AddRows(items);

        ConsoleKey key;
        do
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle($"Delete", ConsoleColor.Yellow);
            Console.WriteLine("Use [↑][↓] to navigate, [←][→] to change page, [Enter] to select and [Escape] to cancel:\n");

            table.Print();

            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: table.MoveUp(); break;
                case ConsoleKey.DownArrow: table.MoveDown(); break;
                case ConsoleKey.LeftArrow: table.PreviousPage(); break;
                case ConsoleKey.RightArrow: table.NextPage(); break;
                case ConsoleKey.Enter:
                    T selected = table.GetSelected();
                    Console.Write("Are you sure you want to delete this item? (y): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        bool success = _deleteItem(selected);
                        Console.WriteLine(success ? "\nDeleted!" : "\nDeletion failed.");
                        Console.ReadKey();
                        return;
                    }
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        } while (true);
    }
}
