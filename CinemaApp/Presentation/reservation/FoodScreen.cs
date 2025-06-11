public class FoodScreen : IScreen
{
    public string ScreenName { get; set; }
    private List<Food> _foods = new();
    private Dictionary<Food, int> _selectedFoods;
    public FoodScreen() => ScreenName = "Select Food Items";

    public void Start()
    {
        _foods = FoodAccess.GetAllAvailableFood();
        _selectedFoods = ReservationLogic.GetSelectedFoodItems();

        Screen();
    }

    public void Screen()
    {
        Table<Food> foodTable = new(maxColWidth: 40, pageSize: 10);
        foodTable.SetColumns("Id", "Name", "Price");
        foodTable.AddRows(_foods);

        ConsoleKey key;

        do
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.White);
            Console.WriteLine();

            // Header
            Console.WriteLine("Use ↑ ↓ to navigate, ← → to change page.");
            Console.WriteLine("Press [SPACE] to add & [BACKSPACE] to remove an item.");
            Console.WriteLine("Press [ENTER] to confirm, [ESC] to return...\n");

            int headerHeight = Console.CursorTop;
            int tableLeft = 0;
            int tableTop = headerHeight;

            int cartLeft = 50;
            int cartTop = headerHeight;

            // Render the food table
            Console.SetCursorPosition(tableLeft, tableTop);
            foodTable.Print("No", "Item", "Price");

            // Render the cart list next to the table
            Console.SetCursorPosition(cartLeft, cartTop);
            Console.WriteLine("Your cart:");

            double totalPrice = 0;
            int cartLine = 1;

            foreach (var selectedFoodKvp in _selectedFoods)
            {
                if (cartTop + cartLine >= Console.WindowHeight - 2)
                    break; // Prevent overflow

                int quantity = selectedFoodKvp.Value;
                Food food = selectedFoodKvp.Key;
                double linePrice = food.Price * quantity;
                totalPrice += linePrice;

                Console.SetCursorPosition(cartLeft, cartTop + cartLine);
                Console.Write($"- {quantity}x {food.Name} (€{linePrice:F2})");

                cartLine++;
            }

            // Print total below cart items
            if (cartTop + cartLine < Console.WindowHeight)
            {
                Console.SetCursorPosition(cartLeft, cartTop + cartLine);
                Console.Write($"Total: €{totalPrice:F2}");
            }

            key = Console.ReadKey(true).Key;
            Food selectedFood = foodTable.GetSelected();

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
                var existingFood = _selectedFoods.Keys.FirstOrDefault(f => f.Id == selectedFood.Id);
                if (existingFood != null)
                    _selectedFoods[existingFood]++;
                else
                    _selectedFoods[selectedFood] = 1;
            }

            if (key == ConsoleKey.Backspace)
            {
                var existingFood = _selectedFoods.Keys.FirstOrDefault(f => f.Id == selectedFood.Id);
                if (existingFood != null)
                {
                    if (_selectedFoods[existingFood] > 1)
                        _selectedFoods[existingFood]--;
                    else
                        _selectedFoods.Remove(existingFood);
                }
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