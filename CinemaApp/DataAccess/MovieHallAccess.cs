using Dapper;
public static class MovieHallAccess
{
    public static List<MovieHall> GetAllMovieHalls()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT id AS Id, name AS Name FROM movie_hall";
            return connection.Query<MovieHall>(sql).ToList();
        }
    }
}
