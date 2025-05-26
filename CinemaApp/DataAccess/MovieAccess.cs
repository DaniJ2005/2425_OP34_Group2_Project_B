using Dapper;

public static class MovieAccess
{
    public static List<Movie> GetAllMovies()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM movie;";
            return connection.Query<Movie>(sql).ToList();
        }
    }
    
    public static Movie GetMovieById(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM movie WHERE id = @Id;";
            return connection.QueryFirstOrDefault<Movie>(sql, new { Id = id });
        }
    }
    
    public static void AddMovie(Movie movie)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"INSERT INTO movie 
                (title, description, genre, duration, language, min_age)
                VALUES 
                (@Title, @Description, @Genre, @Duration, @Language, @MinAgeValue);
                SELECT LAST_INSERT_ID()";
            
            
            int id = connection.ExecuteScalar<int>(sql, new { 
                movie.Title, 
                movie.Description, 
                movie.Genre, 
                movie.Duration, 
                movie.Language,
                movie.MinAge
            });
            
            movie.Id = id;
        }
    }
    
    public static void UpdateMovie(Movie movie)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"UPDATE movie 
                SET title = @Title,
                    description = @Description,
                    genre = @Genre,
                    duration = @Duration,
                    language = @Language,
                    min_age = @MinAgeValue
                WHERE id = @Id";
            
            connection.Execute(sql, new { 
                movie.Title, 
                movie.Description, 
                movie.Genre, 
                movie.Duration, 
                movie.Language,
                movie.MinAge,
                movie.Id 
            });
        }
    }
    
    public static void DeleteMovie(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            // First check if this movie has any sessions
            string checkSql = @"SELECT COUNT(*) FROM movie_session WHERE movie_id = @Id";
            int sessionCount = connection.ExecuteScalar<int>(checkSql, new { Id = id });
            
            if (sessionCount > 0)
            {
                throw new InvalidOperationException("Cannot delete movie with active sessions");
            }
            
            string sql = "DELETE FROM movie WHERE id = @Id";
            connection.Execute(sql, new { Id = id });
        }
    }
}
