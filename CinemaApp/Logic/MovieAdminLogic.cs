public static class MovieAdminLogic
{
    public static List<Movie> GetAllMovies()
    {
        return MovieAccess.GetAllMovies();
    }
    
    public static Movie GetMovieById(int id)
    {
        return MovieAccess.GetMovieById(id);
    }
    
    public static bool AddMovie(Movie movie)
    {
        try
        {
            MovieAccess.AddMovie(movie);
            LoggerLogic.Instance.Log($"Movie added: {movie.Title}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error adding movie: {ex.Message}");
            return false;
        }
    }
    
    public static bool UpdateMovie(Movie movie)
    {
        try
        {
            MovieAccess.UpdateMovie(movie);
            LoggerLogic.Instance.Log($"Movie updated: {movie.Id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error updating movie: {ex.Message}");
            return false;
        }
    }
    
    public static bool DeleteMovie(int id)
    {
        try
        {
            MovieAccess.DeleteMovie(id);
            LoggerLogic.Instance.Log($"Movie deleted: {id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error deleting movie: {ex.Message}");
            return false;
        }
    }
    
    // Methods for movie sessions
    public static List<MovieSession> GetAllMovieSessions()
    {
        return MovieSessionAccess.GetAll();
    }
    
    public static List<MovieSession> GetMovieSessionsByMovieId(int movieId)
    {
        return MovieSessionAccess.GetAllByMovieId(movieId);
    }
    
    public static MovieSession GetMovieSessionById(int id)
    {
        return MovieSessionAccess.GetById(id);
    }
    
    public static bool AddMovieSession(MovieSession session)
    {
        try
        {
            MovieSessionAccess.AddMovieSession(session);
            var sessionDate = DateTime.Parse(session.Date);
            LoggerLogic.Instance.Log($"Movie session added: Movie ID {session.MovieId} at {sessionDate.ToShortDateString()} {session.StartTime}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error adding movie session: {ex.Message}");
            return false;
        }
    }
    
    public static bool UpdateMovieSession(MovieSession session)
    {
        try
        {
            MovieSessionAccess.UpdateMovieSession(session);
            LoggerLogic.Instance.Log($"Movie session updated: {session.Id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error updating movie session: {ex.Message}");
            return false;
        }
    }
    
    public static bool DeleteMovieSession(int id)
    {
        try
        {
            MovieSessionAccess.DeleteMovieSession(id);
            LoggerLogic.Instance.Log($"Movie session deleted: {id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error deleting movie session: {ex.Message}");
            return false;
        }
    }
}