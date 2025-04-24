public class MovieSession : IScreen
{
    public MovieModel _movie { get; private set; }
    public List<MovieSessionModel> _movieSessions { get; private set; }
    public List<string> _uniqueDates { get; private set; }

    public string ScreenName { get; set; }
    public MovieSession() => ScreenName = "MovieSession";

    public void Start()
    {
        ReservationLogic.ClearSession();

        _movie = ReservationLogic.GetSelectedMovie();
        _movieSessions = MovieSessionLogic.GetSessionsForSelectedMovie();
        _uniqueDates = MovieSessionLogic.GetUniqueDatesForSelectedMovie();

        MovieSessionScreen();
    }

    private void MovieSessionScreen()
    {

        if (_movieSessions.Count == 0)
        {
            DisplayMovieInfo();
            Console.WriteLine("No movie sessions available.\n");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            MenuLogic.NavigateToPrevious();
        }
        
        ConsoleKey key;
        int selectedDateIndex = 0;
        int selectedTimeIndex = 0;
        bool isSelectingDate = true;

        do
        {
            Console.Clear();

            DisplayMovieInfo();
            DisplayDateOptions(_uniqueDates, selectedDateIndex, isSelectingDate);

            var selectedDate = _uniqueDates[selectedDateIndex];
            var availableTimes = GetSessionsByDate(selectedDate);

            DisplayAvailableTimes(availableTimes, isSelectingDate ? -1 : selectedTimeIndex);

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                ReservationLogic.ClearSelection();
                MenuLogic.NavigateToPrevious();
            }

            if (isSelectingDate)
            {
                if (key == ConsoleKey.RightArrow && selectedDateIndex < _uniqueDates.Count - 1)
                    selectedDateIndex++;
                else if (key == ConsoleKey.LeftArrow && selectedDateIndex > 0)
                    selectedDateIndex--;
                else if (key == ConsoleKey.DownArrow && availableTimes.Count > 0)
                    isSelectingDate = false;
            }
            else
            {
                if (key == ConsoleKey.DownArrow && selectedTimeIndex < availableTimes.Count - 1)
                    selectedTimeIndex++;
                else if (key == ConsoleKey.UpArrow)
                {
                    if (selectedTimeIndex > 0)
                        selectedTimeIndex--;
                    else
                        isSelectingDate = true; // Go back to date selection
                }
            }

        } while (key != ConsoleKey.Enter);


        // Store movie session
        MovieSessionModel SelectedSession = GetSessionsByDate(_uniqueDates[selectedDateIndex])[selectedTimeIndex];
        ReservationLogic.SetSelectedSession(SelectedSession);
        // Navigate to confirm screen
        MenuLogic.NavigateTo(new ConfirmSelection());
    }

    private void DisplayMovieInfo()
    {
        Console.WriteLine(_movie.Title);
        Console.WriteLine("");
        Console.WriteLine("Description:");
        Console.WriteLine(_movie.Description);
        Console.WriteLine("");
        Console.WriteLine($"Genre: {_movie.Genre} | Language: {_movie.Language}");
        Console.WriteLine($"Duration: {_movie.Duration} | Minimum age: {_movie.MinAge}");
        Console.WriteLine("");
        Console.WriteLine("---------------------------------------------------------------\n");
    }

    private void DisplayDateOptions(List<string> dates, int selectedIndex, bool isSelectingDate)
    {
        for (int i = 0; i < dates.Count; i++)
        {
            if (i == selectedIndex && isSelectingDate)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{dates[i]}]   ");
                Console.ResetColor();
            }
            else
            {
                Console.Write($"{dates[i]}   ");
            }
        }

        Console.WriteLine();
    }


    public void DisplayAvailableTimes(List<MovieSessionModel> sessions, int highlightedIndex)
    {
        Console.WriteLine();

        for (int i = 0; i < sessions.Count; i++)
        {
            if (i == highlightedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"> {sessions[i].StartTime}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {sessions[i].StartTime}");
            }
        }
    }

    private List<MovieSessionModel> GetSessionsByDate(string date)
    {
        return _movieSessions
            .Where(session => session.Date == date)
            .ToList();
    }
}