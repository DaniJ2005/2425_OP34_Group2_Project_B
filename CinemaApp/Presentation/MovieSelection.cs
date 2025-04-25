public class MovieSelection : IScreen
{
    public string ScreenName { get; set; }

    public MovieSelection() => ScreenName = "Movies";
    public void Start()
    {
        ReservationLogic.ClearMovie();

        List<Movie> movies = MovieAccess.GetAllMovies();
        SelectMovie(movies);
    }

    public void SelectMovie(List<Movie> movies)
    {
        int selectedIndex = 0;
        int currentPage = 0;
        int pageSize = 5;
        int totalPages = (movies.Count + pageSize - 1) / pageSize;

        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Select a Movie");
            Console.WriteLine("Use ^ v to navigate, <- -> to change page, [Enter] to select and [Escape] to cancel:\n");

            Console.WriteLine("+----+------------------------------+-----------------+--------+----------+");
            Console.WriteLine("| No | Title                        | Genre           | MinAge | Duration |");
            Console.WriteLine("+----+------------------------------+-----------------+--------+----------+");

            // Movies on the current page
            int start = currentPage * pageSize;
            int end = Math.Min(start + pageSize, movies.Count);

            for (int i = start; i < end; i++)
            {
                var movie = movies[i];
                bool isSelected = i == selectedIndex;

                if (isSelected)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"| {(i + 1),-2} | {Trim(movie.Title, 28),-28} | {Trim(movie.Genre, 15),-15} | {movie.MinAge,6}+ | {Trim(movie.Duration, 8),-8} |");

                if (isSelected)
                    Console.ResetColor();
            }

            Console.WriteLine("+----+------------------------------+-----------------+--------+----------+");
            Console.WriteLine($"Page {currentPage + 1}/{totalPages}");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
            }
            else if (key == ConsoleKey.UpArrow && selectedIndex > start)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < end - 1)
                selectedIndex++;
            else if (key == ConsoleKey.LeftArrow && currentPage > 0)
            {
                currentPage--;
                selectedIndex = currentPage * pageSize;
            }
            else if (key == ConsoleKey.RightArrow && currentPage < totalPages - 1)
            {
                currentPage++;
                selectedIndex = currentPage * pageSize;
            }

        } while (key != ConsoleKey.Enter);

        ReservationLogic.SetSelectedMovie(movies[selectedIndex]);
        MenuLogic.NavigateTo(new MovieSessionScreen());
    }

    private string Trim(string input, int maxLength)
    {
        if (input.Length <= maxLength)
            return input;
        return input.Substring(0, maxLength - 3) + "...";
    }
}
