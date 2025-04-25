using Dapper;

public static class MovieAccess
{
    public static List<Movie> GetAllMovies()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM Movie;";
            return connection.Query<Movie>(sql).ToList();
        }
    }
}