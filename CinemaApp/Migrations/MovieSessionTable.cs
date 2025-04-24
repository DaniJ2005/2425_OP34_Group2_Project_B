using Dapper;
public static class MovieSessionTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE movie_session (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    movie_hall_id INTEGER,
                    movie_id INTEGER,
                    start_time TEXT NOT NULL,
                    date TEXT NOT NULL,
                    FOREIGN KEY (movie_hall_id) REFERENCES movie_hall(id),
                    FOREIGN KEY (movie_id) REFERENCES movie(id)
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
            string checkSql = "SELECT COUNT(*) FROM movie_session;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO movie_session (movie_hall_id, movie_id, start_time, date)
                    VALUES (@MovieHallId, @MovieId, @StartTime, @Date)
                ";

                var MovieSessions = new[]
                {
                    // Movie 1
                    new { MovieHallId = 1, MovieId = 1, StartTime = "16:20", Date = "14/04/25" },
                    new { MovieHallId = 1, MovieId = 1, StartTime = "19:40", Date = "14/04/25" },
                    new { MovieHallId = 1, MovieId = 1, StartTime = "22:40", Date = "14/04/25" },
                    new { MovieHallId = 1, MovieId = 1, StartTime = "22:30", Date = "16/04/25" },
                    new { MovieHallId = 1, MovieId = 1, StartTime = "22:30", Date = "17/04/25" },
                    new { MovieHallId = 1, MovieId = 1, StartTime = "22:30", Date = "19/04/25" },

                    // Movie 2
                    new { MovieHallId = 1, MovieId = 2, StartTime = "14:30", Date = "13/04/25" },
                    new { MovieHallId = 1, MovieId = 2, StartTime = "17:30", Date = "13/04/25" },
                    new { MovieHallId = 1, MovieId = 2, StartTime = "21:30", Date = "13/04/25" },
                    new { MovieHallId = 1, MovieId = 2, StartTime = "14:30", Date = "14/04/25" },
                    new { MovieHallId = 1, MovieId = 2, StartTime = "17:30", Date = "14/04/25" },
                    new { MovieHallId = 1, MovieId = 2, StartTime = "21:30", Date = "14/04/25" },

                };

                connection.Execute(sql, MovieSessions);
            }
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS movie_session;";
            connection.Execute(sql);
        }
    }
}