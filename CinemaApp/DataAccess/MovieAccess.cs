using Dapper;

public static class MovieAccess
{
    public static List<Movie> GetAllMovies()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT id as Id, title as Title, description as Description, genre as Genre, duration as Duration, language as Language, min_age as MinAge FROM movie;";
            return connection.Query<Movie>(sql).ToList();
        }
    }
    
    public static Movie GetMovieById(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT id as Id, title as Title, description as Description, genre as Genre, duration as Duration, language as Language, min_age as MinAge FROM movie WHERE id = @Id;";
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
                (@Title, @Description, @Genre, @Duration, @Language, @MinAgeDb);
                SELECT last_insert_rowid();";

            // Extract the numeric value from MinAge (remove the "+" sign)
            int minAgeValue = int.Parse(movie.MinAge.Replace("+", ""));            
            
            int id = connection.ExecuteScalar<int>(sql, new { 
                movie.Title, 
                movie.Description, 
                movie.Genre, 
                movie.Duration, 
                movie.Language,
                movie.MinAgeDb
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
                    min_age = @MinAgeDb
                WHERE id = @Id";

            // Extract the numeric value from MinAge (remove the "+" sign)
            // int minAgeValue = int.Parse(movie.MinAge.Replace("+", ""));
            
            connection.Execute(sql, new
            {
                movie.Title,
                movie.Description,
                movie.Genre,
                movie.Duration,
                movie.Language,
                movie.MinAgeDb,
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
