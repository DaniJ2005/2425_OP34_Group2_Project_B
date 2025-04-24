using Dapper;

public static class MovieAccess
{
    public static List<MovieModel> GetAllMovies()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM Movie;";
            return connection.Query<MovieModel>(sql).ToList();
        }
    }
}