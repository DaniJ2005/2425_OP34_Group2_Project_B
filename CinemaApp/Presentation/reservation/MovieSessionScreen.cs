public class MovieSessionScreen : IScreen
{
    public Movie _movie { get; private set; }
    public List<MovieSession> _movieSessions { get; private set; }
    public List<string> _uniqueDates { get; private set; }

    public string ScreenName { get; set; }
    public MovieSessionScreen() => ScreenName = "Select a movie session";

    public void Start()
    {
        ReservationLogic.ClearSession();

        _movie = ReservationLogic.GetSelectedMovie();
        _movieSessions = MovieSessionLogic.GetSessionsForSelectedMovie();
        _uniqueDates = MovieSessionLogic.GetUniqueDatesForSelectedMovie();

        Screen();
    }

    private void Screen()
    {

        if (_movieSessions.Count == 0)
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.White);
            Console.WriteLine();
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
            General.ClearConsole();  

            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.White);
            Console.WriteLine();
            DisplayMovieInfo();
            Console.WriteLine("Use [↑][↓][←][→] to navigate, [Enter] to select and [Escape] to return to previous page:\n");
            DisplayDateOptions(_uniqueDates, selectedDateIndex, isSelectingDate);

            var selectedDate = _uniqueDates[selectedDateIndex];
            var availableTimes = GetSessionsByDate(selectedDate);

            DisplayAvailableTimes(availableTimes, isSelectingDate ? -1 : selectedTimeIndex, selectedDate);

            

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
        MovieSession SelectedSession = GetSessionsByDate(_uniqueDates[selectedDateIndex])[selectedTimeIndex];
        ReservationLogic.SetSelectedSession(SelectedSession);
        // Clear seat selection
        SeatLogic.SelectedSeatIds.Clear();
        // Navigate to seat screen
        MenuLogic.NavigateTo(new SeatScreen());
    }

    private void DisplayMovieInfo()
    {
        Console.WriteLine(_movie.Title);
        Console.WriteLine("");
        Console.WriteLine("Description:");
        WriteWrapped(_movie.Description);
        Console.WriteLine("");
        Console.WriteLine($"Genre: {_movie.Genre} | Language: {_movie.Language}");
        Console.WriteLine($"Duration: {_movie.Duration} | Minimum age: {_movie.MinAge}");
        Console.WriteLine("");
        Console.WriteLine("---------------------------------------------------------------\n\n");
    }

    public static void WriteWrapped(string text, int maxWidth = 80)
    {
        var words = text.Split(' ');
        int currentLineLength = 0;

        foreach (var word in words)
        {
            if (currentLineLength + word.Length + 1 > maxWidth)
            {
                Console.WriteLine();
                currentLineLength = 0;
            }

            if (currentLineLength > 0)
            {
                Console.Write(" ");
                currentLineLength++;
            }

            Console.Write(word);
            currentLineLength += word.Length;
        }

        Console.WriteLine(); // Final newline
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


    public void DisplayAvailableTimes(List<MovieSession> sessions, int highlightedIndex, string selectedDate)
    {
        Console.WriteLine();

        string selectedTime = "";

        for (int i = 0; i < sessions.Count; i++)
        {
            if (i == highlightedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"> {sessions[i].StartTime}");
                selectedTime = sessions[i].StartTime;
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {sessions[i].StartTime}");
            }
        }

        Console.WriteLine($"\nCurrently selected: {selectedDate} - {selectedTime}");
    }

    private List<MovieSession> GetSessionsByDate(string date)
    {
        return _movieSessions
            .Where(session => session.Date == date)
            .ToList();
    }
}