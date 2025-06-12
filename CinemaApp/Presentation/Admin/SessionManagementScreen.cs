public class SessionManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Session Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "Add Session", "Update Session", "Delete Session", "View Session" };

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
                    case 0: // Add Session
                        ShowAddSession();
                        break;
                    case 1: // Update Session
                        ShowUpdateSession();
                        break;
                    case 2: // Delete Session
                        ShowDeleteSession();
                        break;
                    case 3: // View Session
                        ShowViewSession();
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

    private void ShowAddSession()
    {
        var movieHalls = MovieAdminLogic.GetAllMovieHalls();
        var movies = MovieAdminLogic.GetAllMovies();

        var movieHallOptions = movieHalls.ToDictionary(h => h.Id.ToString(), h => h.Name);
        var movieOptions = movies.ToDictionary(m => m.Id.ToString(), m => m.Title);

        var movieHallField = new SelectField("MovieHall", movieHallOptions);
        var movieField = new SelectField("Movie", movieOptions);
        var startTimeField = new TimeField("StartTime");

        movieHallField.RenderAndSelect(left: 0, top: 2);
        movieField.RenderAndSelect(left: 0, top: 2 + movieHallOptions.Count + 2);
        startTimeField.RenderAndSelect(left: 0, top: 2 + movieHallOptions.Count + 2 + movieOptions.Count + 3);

        int selectedMovieHallId = int.Parse(movieHallField.Value);
        string selectedStartTime = startTimeField.Value;
        var existingSessions = MovieAdminLogic.GetAllMovieSessions();

        var dateField = new DateField("Date", selectedMovieHallId, selectedStartTime, existingSessions);
        dateField.RenderAndSelect(left: 0, top: 10 + movieHallOptions.Count + movieOptions.Count + 6);

        var fields = new List<FormField>
        {
            movieHallField,
            movieField,
            startTimeField,
            dateField
        };

        var createScreen = new CreateScreen<MovieSession>(
            "Add Session",
            fields,
            () => new MovieSession
            {
                MovieHallId = int.Parse(fields[0].Value),
                MovieId = int.Parse(fields[1].Value),
                StartTime = fields[2].Value,
                Date = fields[3].Value
            },
            MovieAdminLogic.AddMovieSession);

        createScreen.Start();
    }

    private void ShowUpdateSession()
    {
        var sessions = MovieAdminLogic.GetAllMovieSessions();
        if (sessions.Count == 0)
        {
            Console.WriteLine("No Sessions to update.");
            Console.ReadKey();
            return;
        }

        var table = new Table<MovieSession>(maxColWidth: 40, pageSize: 10);
        table.SetColumns("Id", "MovieHallId", "MovieId", "StartTime", "Date");
        table.AddRows(sessions);

        var movieHalls = MovieAdminLogic.GetAllMovieHalls();
        var movies = MovieAdminLogic.GetAllMovies();

        var movieHallOptions = movieHalls.ToDictionary(h => h.Id.ToString(), h => h.Name);
        var movieOptions = movies.ToDictionary(m => m.Id.ToString(), m => m.Title);

        ConsoleKey key;
        do
        {
            General.ClearConsole();
            Console.WriteLine("Select session to update:\n");
            table.Print("Id", "MovieHallId", "MovieId", "StartTime", "Date");
            Console.WriteLine("\n[↑][↓] Navigate  [←][→] Page  [ENTER] Edit  [ESC] Cancel");

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                var selected = table.GetSelected();

                // Use SelectField and specialized fields instead of raw FormField
                var movieHallField = new SelectField("MovieHall", movieHallOptions)
                {
                    Value = selected.MovieHallId.ToString(),
                    OriginalValue = selected.MovieHallId.ToString()
                };
                var movieField = new SelectField("Movie", movieOptions)
                {
                    Value = selected.MovieId.ToString(),
                    OriginalValue = selected.MovieId.ToString()
                };
                var startTimeField = new TimeField("StartTime")
                {
                    Value = selected.StartTime,
                    OriginalValue = selected.StartTime
                };

                // Important: For DateField, we need the movieHallId and startTime as context
                var existingSessions = MovieAdminLogic.GetAllMovieSessions();
                var dateField = new DateField("Date", selected.MovieHallId, selected.StartTime, existingSessions)
                {
                    Value = selected.Date,
                    OriginalValue = selected.Date
                };

                movieHallField.RenderAndSelect(left: 0, top: 2);
                movieField.RenderAndSelect(left: 0, top: 2 + movieHallOptions.Count + 2);
                startTimeField.RenderAndSelect(left: 0, top: 2 + movieHallOptions.Count + 2 + movieOptions.Count + 3);
                dateField.RenderAndSelect(left: 0, top: 10 + movieHallOptions.Count + movieOptions.Count + 6);

                var fields = new List<FormField>
                {
                    movieHallField,
                    movieField,
                    startTimeField,
                    dateField
                };

                var updateScreen = new UpdateScreen<MovieSession>(
                    "Update Session",
                    fields,
                    () => new MovieSession
                    {
                        MovieHallId = int.Parse(fields[0].Value),
                        MovieId = int.Parse(fields[1].Value),
                        StartTime = fields[2].Value,
                        Date = fields[3].Value
                    },
                    MovieAdminLogic.UpdateMovieSession);

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

    private void ShowDeleteSession()
    {
        var deleteScreen = new DeleteScreen<MovieSession>(
            MovieAdminLogic.GetAllMovieSessions,
            m => MovieAdminLogic.DeleteMovieSession(m.Id));

        deleteScreen.Start();
    }

    private void ShowViewSession()
    {
        var readScreen = new ReadScreen<MovieSession>(
            MovieAdminLogic.GetAllMovieSessions,
            new[] { "Id", "MovieHallId", "MovieId", "StartTime", "Date" });

        readScreen.Start();
    }
}
