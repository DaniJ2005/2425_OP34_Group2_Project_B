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
                    // row 1
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 1, Col = 10, IsUnderMaintenance = false },

                    // Row 2
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 2, Col = 11, IsUnderMaintenance = false },

                    // Row 3
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 3, Col = 11, IsUnderMaintenance = false },

                    // Row 4
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 4, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 4, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 4, Col = 12, IsUnderMaintenance = false },

                    // Row 5
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 5, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 5, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 5, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 5, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 5, Col = 12, IsUnderMaintenance = false },

                    // Row 6
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 6, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 6, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 6, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 6, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 6, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 6, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 6, Col = 12, IsUnderMaintenance = false },

                    // Row 7
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 7, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 7, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 7, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 7, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 7, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 7, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 7, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 7, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 7, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 7, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 7, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 7, Col = 12, IsUnderMaintenance = false },

                    // Row 8
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 8, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 8, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 8, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 8, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 8, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 8, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 8, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 8, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 8, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 8, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 8, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 8, Col = 12, IsUnderMaintenance = false },

                    // Row 9
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 9, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 9, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 9, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 9, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 9, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 9, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 1, Row = 9, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 9, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 9, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 9, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 9, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 9, Col = 12, IsUnderMaintenance = false },

                    // Row 10
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 10, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 10, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 10, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 10, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 10, Col = 12, IsUnderMaintenance = false },

                    // Row 11
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 11, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 1, Row = 11, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 11, Col = 12, IsUnderMaintenance = false },

                    // Row 12
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 12, Col = 11, IsUnderMaintenance = false },

                    // Row 13
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 13, Col = 10, IsUnderMaintenance = false },

                    // Row 14
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 1, Row = 14, Col = 10, IsUnderMaintenance = false },

                    // ---- Hall 2 ----------------------------------------------------------------------

                    // ---- Row 1 -----------------------------------------------------------------------
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
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 1, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 2 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 2, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 2, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 2, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 2, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 2, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 2, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 2, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 3 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 3, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 3, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 4 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 4, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 4, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 5 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 5, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 5, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 6 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 6, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 6, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 6, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 6, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 7 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 7, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 7, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 7, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 7, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 7, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 7, Col = 18, IsUnderMaintenance = false },


                    // ---- Row 8 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 8, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 8, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 8, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 2, Row = 8, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 8, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 8, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 8, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 8, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 8, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 8, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 8, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 8, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 8, Col = 18, IsUnderMaintenance = false },


                    // ---- Row 9 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 9, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 9, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 2, Row = 9, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 9, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 9, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 9, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 9, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 9, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 9, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 9, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 9, Col = 18, IsUnderMaintenance = false },


                    // ---- Row 10 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 10, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 10, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 2, Row = 10, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 10, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 10, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 10, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 10, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 10, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 10, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 10, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 10, Col = 18, IsUnderMaintenance = false },


                    // ---- Row 11 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 11, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 11, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 2, Row = 11, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 11, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 11, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 11, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 11, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 11, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 11, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 11, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 11, Col = 18, IsUnderMaintenance = false },


                    // ---- Row 12 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 12, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 12, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 12, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 12, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 12, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 12, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 12, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 12, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 12, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 13 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 13, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 13, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 13, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 13, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 2, Row = 13, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 13, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 13, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 13, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 13, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 14 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 14, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 14, Col = 17, IsUnderMaintenance = false },


                    // ---- Row 15 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 15, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 15, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 15, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 15, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 15, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 15, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 15, Col = 16, IsUnderMaintenance = false },


                    // ---- Row 16 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 2, Row = 16, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 16, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 16, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 16, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 16, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 2, Row = 16, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 16, Col = 16, IsUnderMaintenance = false },


                    // ---- Row 17 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 17, Col = 16, IsUnderMaintenance = false },


                    // ---- Row 18 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 18, Col = 15, IsUnderMaintenance = false },


                    // ---- Row 19 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 6, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 11, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 12, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 2, Row = 19, Col = 15, IsUnderMaintenance = false },


                    // ---- Hall 3 ----------------------------------------------------------------------

                    // ---- Row 1 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 1, Col = 26, IsUnderMaintenance = false },


                    // ---- Row 2 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 2, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 2, Col = 27, IsUnderMaintenance = false },
                    

                    // ---- Row 3 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 3, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 3, Col = 27, IsUnderMaintenance = false },
                    

                    // ---- Row 4 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 4, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 4, Col = 27, IsUnderMaintenance = false },
                    

                    // ---- Row 5 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 5, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 5, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 5, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 5, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 5, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 5, Col = 27, IsUnderMaintenance = false },
                    

                    // ---- Row 6 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 6, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 6, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 6, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 6, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 6, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 6, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 6, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 6, Col = 28, IsUnderMaintenance = false },
                    

                    // ---- Row 7 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 7, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 7, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 7, Col = 29, IsUnderMaintenance = false },
                    

                    // ---- Row 8 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 8, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 8, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 29, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 8, Col = 30, IsUnderMaintenance = false },
                    

                    // ---- Row 9 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 9, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 9, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 29, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 9, Col = 30, IsUnderMaintenance = false },
                    

                    // ---- Row 10 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 10, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 10, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 29, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 10, Col = 30, IsUnderMaintenance = false },
                    

                    // ---- Row 11 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 11, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 11, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 29, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 11, Col = 30, IsUnderMaintenance = false },

                    
                    // ---- Row 12 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 1, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 12, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 12, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 29, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 12, Col = 30, IsUnderMaintenance = false },
                    

                    // ---- Row 13 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 2, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 13, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 13, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 13, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 3, MovieHallId = 3, Row = 13, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 13, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 28, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 13, Col = 29, IsUnderMaintenance = false },
                    

                    // ---- Row 14 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 14, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 14, Col = 28, IsUnderMaintenance = false },
                    

                    // ---- Row 15 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 3, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 15, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 27, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 15, Col = 28, IsUnderMaintenance = false },
                    

                    // ---- Row 16 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 2, MovieHallId = 3, Row = 16, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 16, Col = 27, IsUnderMaintenance = false },
                    

                    // ---- Row 17 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 4, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 5, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 17, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 17, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 17, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 17, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 17, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 2, MovieHallId = 3, Row = 17, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 25, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 26, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 17, Col = 27, IsUnderMaintenance = false },
                    

                    // ---- Row 18 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 6, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 7, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 23, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 24, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 18, Col = 25, IsUnderMaintenance = false },
                    

                    // ---- Row 19 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 8, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 19, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 22, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 19, Col = 23, IsUnderMaintenance = false },
                    

                    // ---- Row 20 -----------------------------------------------------------------------
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 9, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 10, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 11, IsUnderMaintenance = false },

                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 12, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 13, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 14, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 15, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 16, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 17, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 18, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 19, IsUnderMaintenance = false },
                    
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 20, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 21, IsUnderMaintenance = false },
                    new { SeatTypeId = 1, MovieHallId = 3, Row = 20, Col = 22, IsUnderMaintenance = false },


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