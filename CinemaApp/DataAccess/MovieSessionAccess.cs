using Dapper;

public static class MovieSessionAccess
{
    public static List<MovieSession> GetAllByMovieId(int movieId)
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
            return connection.Query<MovieSession>(sql, new { MovieId = movieId }).ToList();
        }
    }

    public static List<MovieSession> GetAll()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT
                movie_session.id AS Id,
                movie_hall.name AS MovieHallName, 
                movie.title AS MovieTitle,
                movie_session.start_time AS StartTime,
                movie_session.date AS Date
                FROM movie_session
                INNER JOIN movie ON movie_session.movie_id = movie.id
                INNER JOIN movie_hall ON movie_session.movie_hall_id = movie_hall.id
            ";
            return connection.Query<MovieSession>(sql).ToList();
        }
    }

    public static MovieSession GetById(int id)
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
                FROM movie_session
                WHERE id = @Id";
            return connection.QueryFirstOrDefault<MovieSession>(sql, new { Id = id });
        }
    }

    public static void AddMovieSession(MovieSession session)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"INSERT INTO movie_session 
                (movie_hall_id, movie_id, start_time, date)
                VALUES 
                (@MovieHallId, @MovieId, @StartTime, @Date);
                SELECT last_insert_rowid();";

            int id = connection.ExecuteScalar<int>(sql, new
            {
                session.MovieHallId,
                session.MovieId,
                session.StartTime,
                session.Date
            });

            session.Id = id;
        }
    }

    public static void UpdateMovieSession(MovieSession session)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"UPDATE movie_session 
                SET movie_hall_id = @MovieHallId,
                    movie_id = @MovieId,
                    start_time = @StartTime,
                    date = @Date
                WHERE id = @Id";

            connection.Execute(sql, new
            {
                session.MovieHallId,
                session.MovieId,
                session.StartTime,
                session.Date,
                session.Id
            });
        }
    }

    public static void DeleteMovieSession(int id)
    {
        try
        {
            using (var connection = Db.CreateConnection())
            {
                connection.Execute("PRAGMA foreign_keys = ON;");

                // Check if there are any tickets sold for this session
                string checkSql = @"SELECT COUNT(*) FROM reservation WHERE movie_session_id = @Id";
                int ticketCount = connection.ExecuteScalar<int>(checkSql, new { Id = id });

                LoggerLogic.Instance.Log($"----------- {id}");

                if (ticketCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete movie session with sold tickets");
                }

                string sql = "DELETE FROM movie_session WHERE id = @Id";
                int rowsAffected = connection.Execute(sql, new { Id = id });

                if (rowsAffected == 0)
                {
                    throw new Exception($"No movie session found with ID {id} to delete.");
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            LoggerLogic.Instance.Log($"DeleteMovieSession failed: {ex.Message}");
            throw;  // rethrow if you want the caller to handle it too
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Unexpected error deleting movie session ID {id}: {ex.Message}");
            throw;  // or handle error gracefully here, depending on your design
        }
    }

    public static List<MovieSession> GetAllByMovieHallId(int movieHallId, string date)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT 
                start_time AS StartTime, 
                date AS Date,
                movie.duration as MovieDuration
                FROM movie_session
                INNER JOIN movie ON movie_session.movie_id == movie.id
                WHERE movie_hall_id == @MovieHallId AND date == @Date";
            return connection.Query<MovieSession>(sql, new { MovieHallId = movieHallId, Date = date }).ToList();
        }
    }
}