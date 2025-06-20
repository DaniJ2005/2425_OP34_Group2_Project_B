public class MovieManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Movie Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "Add Movie", "Update Movie", "Delete Movie", "View Movies" };

        do
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.Yellow);
            Console.WriteLine("[↑][↓] to navigate, [ENTER] to select, [ESC] to go back\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0) selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1) selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                General.ClearConsole();
                switch (selectedIndex)
                {
                    case 0: // Add Movie
                        ShowAddMovie();
                        break;
                    case 1: // Update Movie
                        ShowUpdateMovie();
                        break;
                    case 2: // Delete Movie
                        ShowDeleteMovie();
                        break;
                    case 3: // View Movies
                        ShowViewMovies();
                        break;
                }
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                return;
            }
        } while (true);
    }

    private void ShowAddMovie()
    {
        var fields = new List<FormField>
        {
            new("Title", false, v => (!string.IsNullOrWhiteSpace(v), "Title required")),
            new("Description"),
            new("Genre"),
            new("Duration", false, ValidateDuration),
            new("Language"),
            new("Min Age", false, v => (int.TryParse(v, out _), "Must be a number"))
        };

        var createScreen = new CreateScreen<Movie>(
            "Add Movie",
            fields,
            () => new Movie
            {
                Title = fields[0].Value,
                Description = fields[1].Value,
                Genre = fields[2].Value,
                Duration = fields[3].Value,
                Language = fields[4].Value,
                MinAge = fields[5].Value
            },
            MovieAdminLogic.AddMovie);

        createScreen.Start();
    }
    
    private (bool isValid, string errorMessage) ValidateDuration(string input)
    {
        if (!TimeSpan.TryParseExact(input, @"hh\:mm", null, out TimeSpan duration))
            return (false, "Duration must be in HH:mm format");

        if (duration > TimeSpan.FromHours(4))
            return (false, "Duration cannot exceed 4 hours");

        return (true, "");
    }

    private void ShowUpdateMovie()
    {
        var movies = MovieAdminLogic.GetAllMovies();
        if (movies.Count == 0)
        {
            Console.WriteLine("No movies to update.");
            Console.ReadKey();
            return;
        }

        var table = new Table<Movie>(maxColWidth: 40, pageSize: 10);
        table.SetColumns("Id", "Title", "Description", "Genre", "Duration", "MinAge", "Language");
        table.AddRows(movies);

        ConsoleKey key;
        do
        {
            General.ClearConsole();
            Console.WriteLine("Select movie to update:\n");
            table.Print("Id", "Title", "Description", "Genre", "Duration", "MinAge", "Language");
            Console.WriteLine("\n[↑][↓] Navigate  [←][→] Page  [ENTER] Edit  [ESC] Cancel");

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                var selected = table.GetSelected();

                var fields = new List<FormField>
                {
                    new("Title", false, v => (!string.IsNullOrWhiteSpace(v), "Title required"))
                        { Value = selected.Title, OriginalValue = selected.Title },
                    new("Description")
                        { Value = selected.Description, OriginalValue = selected.Description },
                    new("Genre")
                        { Value = selected.Genre, OriginalValue = selected.Genre },
                    new("Language")
                        { Value = selected.Language, OriginalValue = selected.Language },
                    new("Min Age", false, v => (int.TryParse(v, out _), "Must be a valid number (without +)"))
                        { Value = selected.MinAgeDb.ToString(), OriginalValue = selected.MinAgeDb.ToString() }
                };

                var updateScreen = new UpdateScreen<Movie>(
                    "Update Movie",
                    fields,
                    () => new Movie
                    {
                        Id = selected.Id,
                        Title = fields[0].Value,
                        Description = fields[1].Value,
                        Genre = fields[2].Value,
                        Duration = selected.Duration,
                        Language = fields[3].Value,
                        MinAgeDb = int.Parse(fields[4].Value.Replace("+", ""))
                    },
                    MovieAdminLogic.UpdateMovie);

                MenuLogic.NavigateTo(updateScreen);
                return;
            }
            else if (key == ConsoleKey.UpArrow) table.MoveUp();
            else if (key == ConsoleKey.DownArrow) table.MoveDown();
            else if (key == ConsoleKey.LeftArrow) table.PreviousPage();
            else if (key == ConsoleKey.RightArrow) table.NextPage();
            else if (key == ConsoleKey.Escape) return;

        } while (true);
    }

    private void ShowDeleteMovie()
    {
        var deleteScreen = new DeleteScreen<Movie>(
            MovieAdminLogic.GetAllMovies,
            m => MovieAdminLogic.DeleteMovie(m.Id),
            new string[] { "Id", "Title", "Description", "Genre", "Duration", "MinAge", "Language" });

        deleteScreen.Start();
    }

    private void ShowViewMovies()
    {
        var readScreen = new ReadScreen<Movie>(
            MovieAdminLogic.GetAllMovies,
            new[] { "Id", "Title", "Description", "Genre", "Duration", "MinAge", "Language" });

        readScreen.Start();
    }
}

