public static class MovieSession
{
    private static MovieModel movie = ReservationLogic.GetSelectedMovie();
    private static List<MovieSessionModel> movieSessions = MovieSessionAccess.GetAllByMovieId(movie.Id);
    private static List<string> uniqueDates = movieSessions.Select(p => p.Date).Distinct().ToList();


    public static void Start()
    {
        MovieSessionScreen();
    }

    private static void MovieSessionScreen()
    {
        ConsoleKey key;
        int selectedDateIndex = 0;
        int selectedTimeIndex = 0;
        bool isSelectingDate = true;

        do
        {
            Console.Clear();

            DisplayMovieInfo();
            DisplayDateOptions(uniqueDates, selectedDateIndex, isSelectingDate);

            var selectedDate = uniqueDates[selectedDateIndex];
            var availableTimes = GetSessionsByDate(selectedDate);

            DisplayAvailableTimes(availableTimes, isSelectingDate ? -1 : selectedTimeIndex);

            key = Console.ReadKey(true).Key;

            if (isSelectingDate)
            {
                if (key == ConsoleKey.RightArrow && selectedDateIndex < uniqueDates.Count - 1)
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
        MovieSessionModel SelectedSession = GetSessionsByDate(uniqueDates[selectedDateIndex])[selectedTimeIndex];
        ReservationLogic.SetSelectedSession(SelectedSession);
    }

    private static void DisplayMovieInfo()
    {
        Console.WriteLine(movie.Title);
        Console.WriteLine(movie.Description);
        Console.WriteLine("");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("");
    }

    private static void DisplayDateOptions(List<string> dates, int selectedIndex, bool isSelectingDate)
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


    public static void DisplayAvailableTimes(List<MovieSessionModel> sessions, int highlightedIndex)
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

    private static List<MovieSessionModel> GetSessionsByDate(string date)
    {
        return movieSessions
            .Where(session => session.Date == date)
            .ToList();
    }
}