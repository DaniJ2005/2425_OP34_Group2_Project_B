public class FoodScreen : IScreen
{
    public string ScreenName { get; set; }
    private List<Food> _foods = new();
    public FoodScreen() => ScreenName = "Food";

    public void Start()
    {
        _foods = FoodAccess.GetAllFood();
        SelectFood();
    }

    public void SelectFood()
    {
        Table<Food> foodTable = new(maxColWidth: 40, pageSize: 10);
        foodTable.SetColumns("Id", "Name", "Price", "Is_Available");

        foodTable.AddRows(_foods);

        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Select food and drinks");
            Console.WriteLine("Use ^ v to navigate, <- -> to change page, [Enter] to select and [Escape] to cancel:\n");

            foodTable.Print("No", "Item", "Price", "Available");

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
            

        } while (key != ConsoleKey.Enter);

        Food selectedFood = foodTable.GetSelected();

        if (selectedFood == null)
        {
            MenuLogic.RestartScreen();
        }

        //ReservationLogic.SetSelectedMovie(selectedMovie);
        MenuLogic.NavigateTo(new MovieSessionScreen());
    }


    private string Trim(string input, int maxLength)
    {
        if (input.Length <= maxLength)
            return input;
        return input.Substring(0, maxLength - 3) + "...";
    }

}