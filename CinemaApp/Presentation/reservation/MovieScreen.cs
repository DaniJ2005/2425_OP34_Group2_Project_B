public class MovieScreen : IScreen
{
    public string ScreenName { get; set; }
    private List<Movie> _movies = new();

    public MovieScreen() => ScreenName = "Select a Movie";
    public void Start()
    {
        ReservationLogic.ClearMovie();
        ReservationLogic.ClearFood();

        // Move Access call to logic
        _movies = MovieAccess.GetAllMovies();
        SelectMovie();
    }

    public void SelectMovie()
    {
        Table<Movie> movieTable = new(maxColWidth: 40, pageSize: 10);
        movieTable.SetColumns("Title", "Genre", "MinAge", "Duration");

        movieTable.AddRows(_movies);

        ConsoleKey key;

        do
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.White);
            Console.WriteLine("Use [↑][↓] to navigate, [←][→] to change page, [Enter] to select and [Escape] to cancel:\n");

            movieTable.Print("Movie Title", "Gerne", "Age", "Duration");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
                MenuLogic.NavigateToPrevious();
            
            if (key == ConsoleKey.UpArrow)
                movieTable.MoveUp();
            if (key == ConsoleKey.DownArrow)
                movieTable.MoveDown();
            if (key == ConsoleKey.RightArrow)
                movieTable.NextPage();
            if (key == ConsoleKey.LeftArrow)
                movieTable.PreviousPage();
            

        } while (key != ConsoleKey.Enter);

        Movie selectedMovie = movieTable.GetSelected();

        if (selectedMovie == null)
        {
            MenuLogic.RestartScreen();
        }

        ReservationLogic.SetSelectedMovie(selectedMovie);
        MenuLogic.NavigateTo(new MovieSessionScreen());
    }

}
