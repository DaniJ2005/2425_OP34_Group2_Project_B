using System;

public class MovieManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Movie Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "Add Movie", "Update Movie", "Delete Movie", "View Movies", "Back" };
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       MOVIE MANAGEMENT       ║");
            Console.WriteLine("╚══════════════════════════════╝");
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

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0: // Add Movie
                        AddMovieUI();
                        break;
                    case 1: // Update Movie
                        UpdateMovieUI();
                        break;
                    case 2: // Delete Movie
                        DeleteMovieUI();
                        break;
                    case 3: // View Movies
                        ViewMoviesUI();
                        break;
                    case 4: // Back
                        MenuLogic.NavigateToPrevious();
                        return;
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("User pressed Escape - returning to admin menu");
                return;
            }
        } while (true);
    }

    private void AddMovieUI()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║          ADD MOVIE           ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.WriteLine();

        var movie = new Movie();

        Console.Write("Title: ");
        movie.Title = Console.ReadLine();

        Console.Write("Description: ");
        movie.Description = Console.ReadLine();

        Console.Write("Genre: ");
        movie.Genre = Console.ReadLine();

        Console.Write("Duration (e.g. 120 min): ");
        movie.Duration = Console.ReadLine();

        Console.Write("Language: ");
        movie.Language = Console.ReadLine();

        Console.Write("Minimum Age (e.g. 13): ");
        int minAge;
        if (int.TryParse(Console.ReadLine(), out minAge))
        {
            movie.MinAge = minAge.ToString();
        }
        else
        {
            Console.WriteLine("Invalid age format. Using default of 0+");
            movie.MinAge = "0";
        }

        bool success = MovieAdminLogic.AddMovie(movie);
        Console.WriteLine(success ? "\nFailed to add movie!" : "\nMovie added successfully!");
    }

    private void UpdateMovieUI()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║         UPDATE MOVIE         ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.WriteLine();

        ViewMoviesUI();
        Console.Write("\nEnter ID of movie to update: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid ID format!");
            return;
        }

        var movie = MovieAdminLogic.GetMovieById(id);
        if (movie == null)
        {
            Console.WriteLine("Movie not found!");
            return;
        }

        Console.WriteLine("\nLeave blank to keep current value\n");

        Console.Write($"Title ({movie.Title}): ");
        string title = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(title)) movie.Title = title;

        Console.Write($"Description ({movie.Description}): ");
        string description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description)) movie.Description = description;

        Console.Write($"Genre ({movie.Genre}): ");
        string genre = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(genre)) movie.Genre = genre;

        Console.Write($"Duration ({movie.Duration}): ");
        string duration = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(duration)) movie.Duration = duration;

        Console.Write($"Language ({movie.Language}): ");
        string language = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(language)) movie.Language = language;

        Console.Write($"Minimum Age ({movie.MinAge}): ");
        string minAge = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(minAge))
        {
            // Remove any "+" character that might be present
            minAge = minAge.Replace("+", "");
            if (int.TryParse(minAge, out int age))
            {
                movie.MinAge = age.ToString();
            }
            else
            {
                Console.WriteLine("Invalid age format. Keeping current value.");
            }
        }

        bool success = MovieAdminLogic.UpdateMovie(movie);
        Console.WriteLine(success ? "\nMovie updated successfully!" : "\nFailed to update movie!");
    }

    private void DeleteMovieUI()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║         DELETE MOVIE         ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.WriteLine();

        ViewMoviesUI();
        Console.Write("\nEnter ID of movie to delete: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid ID format!");
            return;
        }

        Console.Write("Are you sure? This will delete all associated sessions. (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            bool success = MovieAdminLogic.DeleteMovie(id);
            Console.WriteLine(success ? "\nMovie deleted successfully!" : "\nFailed to delete movie! (Movie may have active sessions)");
        }
    }

    private void ViewMoviesUI()
    {
        var movies = MovieAdminLogic.GetAllMovies();
        
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                               MOVIES LIST                              ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
        
        if (movies.Count == 0)
        {
            Console.WriteLine("\nNo movies found in the database.");
            return;
        }
        
        Console.WriteLine("\nID   | Title                          | Genre           | Duration | Age  | Language");
        Console.WriteLine("-----+--------------------------------+-----------------+----------+------+---------");
        
        foreach (var movie in movies)
        {
            Console.WriteLine($"{movie.Id,-5}| {TruncateString(movie.Title, 30),-30} | {TruncateString(movie.Genre, 15),-15} | {movie.Duration,-8} | {movie.MinAge,-4} | {movie.Language}");
        }
    }
    
    private string TruncateString(string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        return str.Length <= maxLength ? str : str.Substring(0, maxLength - 3) + "...";
    }
}