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
                    seat_type_id INTEGER,
                    movie_hall_id INTEGER,
                    row TEXT NOT NULL,
                    number INTEGER NOT NULL,
                    is_under_maintenance BOOLEAN NOT NULL DEFAULT 0,
                    FOREIGN KEY (seat_type_id) REFERENCES seat_type(id),
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
