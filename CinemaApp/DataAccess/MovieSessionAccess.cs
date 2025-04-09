using Dapper;

public static class MovieSessionAccess
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

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS movie_session;";
            connection.Execute(sql);
        }
    }
}
