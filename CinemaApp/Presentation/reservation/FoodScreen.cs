public class FoodScreen : IScreen
{
    public string ScreenName { get; set; }
    private List<Food> _foods = new();
    private List<Food> _selectedFoods = new();
    public FoodScreen() => ScreenName = "Food";

    public void Start()
    {
        ReservationLogic.ClearFood();
        _foods = FoodAccess.GetAllAvailableFood();
        SelectFood();
    }

    public void SelectFood()
    {
        Table<Food> foodTable = new(maxColWidth: 40, pageSize: 10);
        foodTable.SetColumns("Id", "Name", "Price");

        foodTable.AddRows(_foods);

        ConsoleKey key;

        do
        {
            General.ClearConsole();  
            Console.WriteLine("Select food and drinks");
            Console.WriteLine("Use ^ v to navigate, <- -> to change page, [Enter] to select and [Escape] to cancel:\n");
            Console.WriteLine("");
            
             Dictionary<Food,int> quantities = new Dictionary<Food,int>();
            
            foreach (var food in _selectedFoods)
            {
                if (quantities.ContainsKey(food))
                {
                    quantities[food]++;
                }
                else
                {
                    quantities[food] = 1;
                }
            }
            Console.WriteLine("Your cart: ");
            foreach (var kv in quantities)
            {
                Console.WriteLine($"- x{kv.Value} - {kv.Key.Name} €{kv.Key.Price * kv.Value}");
            }

            double total = 0;
            foreach (var food in _selectedFoods)
            {
                total += food.Price;
            }
            Console.WriteLine($"Total: €{total}");

            foodTable.Print("No", "Item", "Price");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
                MenuLogic.NavigateToPrevious();
            
            if (key == ConsoleKey.UpArrow)
                foodTable.MoveUp();
            if (key == ConsoleKey.DownArrow)
                foodTable.MoveDown();
            if (key == ConsoleKey.RightArrow)
                foodTable.NextPage();
            if (key == ConsoleKey.LeftArrow)
                foodTable.PreviousPage();
            if (key == ConsoleKey.Spacebar)
            {
                _selectedFoods.Add(foodTable.GetSelected());
            }
            if (key == ConsoleKey.Backspace)
            {
                
            }

        } while (key != ConsoleKey.Enter);


        if (_selectedFoods.Count == 0)
        {
            MenuLogic.RestartScreen();
        }

        ReservationLogic.SetSelectedFoodItems(_selectedFoods); 
        MenuLogic.NavigateToPrevious();
    }

}