using Dapper;

public static class SeatAccess
{

    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE seat (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    movie_hall_id INTEGER,
                    seat_type TEXT NOT NULL,
                    base_price REAL NOT NULL,
                    row TEXT NOT NULL,
                    number INTEGER NOT NULL,
                    is_under_maintenance BOOLEAN NOT NULL DEFAULT 0,
                    FOREIGN KEY (movie_hall_id) REFERENCES movie_hall(id)
                );
            ";

            connection.Execute(sql);
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS seat;";
            connection.Execute(sql);
        }
    }
}
