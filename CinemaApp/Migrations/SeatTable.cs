using Dapper;
public static class SeatTable
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

    public static void PopulateTable()
    {
        using (var connection = Db.CreateConnection())
        {
            // Check if the table is empty
            string checkSql = "SELECT COUNT(*) FROM seat;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO seat (seat_type_id, movie_hall_id, row, number, is_under_maintenance) 
                    VALUES (@SeatTypeId, @MovieHallId, @Row, @Number, @IsUnderMaintenance);
                ";

                var Seats = new[]
                {
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "A", Number = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "A", Number = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "A", Number = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "A", Number = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "B", Number = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "B", Number = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "B", Number = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = "B", Number = 4, IsUnderMaintenance = false },
                };

                connection.Execute(sql, Seats);
            }
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