public class DeleteScreen<T>
{
    private readonly Func<List<T>> _loadItems;
    private readonly Func<T, bool> _deleteItem;

    public DeleteScreen(Func<List<T>> loadItems, Func<T, bool> deleteItem)
    {
        _loadItems = loadItems;
        _deleteItem = deleteItem;
    }

    public void Start()
    {
        var items = _loadItems();
        var table = new Table<T>(30, 10);
        table.AddRows(items);

        ConsoleKey key;
        do
        {
            General.ClearConsole();
            Console.WriteLine("Select item to delete:\n");
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
                    Console.Write("Are you sure you want to delete this item? (y/n): ");
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
