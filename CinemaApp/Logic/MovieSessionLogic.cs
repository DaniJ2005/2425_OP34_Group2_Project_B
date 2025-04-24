static class MovieSessionLogic
{
    public static List<MovieSessionModel> GetSessionsForSelectedMovie()
    {
        var movie = ReservationLogic.GetSelectedMovie();
        return MovieSessionAccess.GetAllByMovieId(movie.Id);
    }

    public static List<string> GetUniqueDatesForSelectedMovie()
    {
        return GetSessionsForSelectedMovie()
            .Select(s => s.Date)
            .Distinct()
            .ToList();
    }

    public static List<MovieSessionModel> GetSessionsByDate(string date)
    {
        return GetSessionsForSelectedMovie()
            .Where(s => s.Date == date)
            .ToList();
    }
}