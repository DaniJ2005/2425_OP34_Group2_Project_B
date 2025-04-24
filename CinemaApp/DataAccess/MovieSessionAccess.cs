using Dapper;

public static class MovieSessionAccess
{
    public static List<MovieSessionModel> GetAllByMovieId(int movieId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                            id AS Id,
                            movie_hall_id AS MovieHallId,
                            movie_id AS MovieId,
                            start_time AS StartTime,
                            date AS Date
                        FROM movie_session WHERE movie_id = @MovieId";
            return connection.Query<MovieSessionModel>(sql, new { MovieId = movieId }).ToList();
        }
    }

    public static List<MovieSessionModel> GetAll()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT 
                    id AS Id,
                    movie_hall_id AS MovieHallId,
                    movie_id AS MovieId,
                    start_time AS StartTime,
                    date AS Date
                FROM movie_session";

            return connection.Query<MovieSessionModel>(sql).ToList();
        }
    }
}
