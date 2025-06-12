using Dapper;
public static class SeatTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS seat (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    seat_type_id INTEGER,
                    movie_hall_id INTEGER,
                    row INTEGER NOT NULL,
                    col INTEGER NOT NULL,
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
                    INSERT INTO seat (seat_type_id, movie_hall_id, row, col, is_under_maintenance) 
                    VALUES (@SeatTypeId, @MovieHallId, @Row, @Col, @IsUnderMaintenance);
                ";

                var Seats = new[]
                {
                    // Hall 1
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 7, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 2, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 2, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 8, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 3, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 3, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 3, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 3, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 8, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 4, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 4, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 4, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 4, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 8, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 5, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 5, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 8, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 7, IsUnderMaintenance = false },

                    // Hall 2
                    // Row 1
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 11, IsUnderMaintenance = false },

                    // Row 2
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 12, IsUnderMaintenance = false },

                    // Row 3
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 12, IsUnderMaintenance = false },

                    // Row 4
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 12, IsUnderMaintenance = false },

                    // Row 5
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 12, IsUnderMaintenance = false },

                    // Row 6
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 12, IsUnderMaintenance = false },

                    // Row 7
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 12, IsUnderMaintenance = false },

                    // Row 8
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 11, IsUnderMaintenance = false },
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