public class FoodScreen : IScreen
{
    public string ScreenName { get; set; }
    private List<Food> _foods = new();
    private Dictionary<Food, int> _selectedFoods;
    public FoodScreen() => ScreenName = "Food";

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
            Console.WriteLine("Select food and drinks!\n");
            Console.WriteLine("Use ^ v to navigate, <- -> to change page,");
            Console.WriteLine("Press [SPACE] to add & [BACKSPACE] to remove an item,");
            Console.WriteLine("Press [ESC] to return...");
            Console.WriteLine("");
            
            Console.WriteLine("Your cart:\n");

            double totalPrice = 0;
            
            foreach (var selectedFoodKvp in _selectedFoods)
            {
                int quantity = selectedFoodKvp.Value;
                Food food = selectedFoodKvp.Key;

                totalPrice += food.Price * quantity;

                Console.WriteLine($" - {quantity}x {food.Name} - €{food.Price * quantity:F2}");
            }

            Console.WriteLine($"\nTotal: €{totalPrice:F2}");

            foodTable.Print("No", "Item", "Price");

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
                {
                    _selectedFoods[existingFood]++;
                }
                else
                {
                    _selectedFoods[selectedFood] = 1;
                }
            }
            if (key == ConsoleKey.Backspace && _selectedFoods.ContainsKey(selectedFood))
            {
                if (_selectedFoods[selectedFood] > 1)
                {
                    _selectedFoods[selectedFood]--;
                }
                else
                {
                    _selectedFoods.Remove(selectedFood);
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