using Dapper;
public static class MovieTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE movie (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    description TEXT,
                    genre TEXT,
                    duration TEXT,
                    language TEXT,
                    min_age INTEGER
                );
            ";

            connection.Execute(sql);
        }
    }

    public static void PopulateTable()
    {
        using (var connection = Db.CreateConnection())
        {
            // Check if the table is empty
            string checkSql = "SELECT COUNT(*) FROM movie;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO movie (title, description, genre, duration, language, min_age) 
                    VALUES (@Title, @Description, @Genre, @Duration, @Language, @MinAge)
                ";

                var Movies = new[]
                {
                    new { Title = "A Minecraft Movie", Description = "Desc", Genre = "Actie/Avontuur/Familiefilm", Duration = "1h 41m", Language = "EN", MinAge = 9}
                };

                connection.Execute(sql, Movies);
            }
        }
    }

    

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS movie;";
            connection.Execute(sql);
        }
    }
}