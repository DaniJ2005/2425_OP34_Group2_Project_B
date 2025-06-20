using Dapper;
public static class TicketTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS ticket (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    seat_id INTEGER,
                    reservation_id INTEGER,
                    seat_price_id INTEGER,
                    FOREIGN KEY (seat_id) REFERENCES seat(id),
                    FOREIGN KEY (reservation_id) REFERENCES reservation(id),
                    FOREIGN KEY (seat_price_id) REFERENCES seat_price(id)
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
            string checkSql = "SELECT COUNT(*) FROM ticket;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO ticket (reservation_id, seat_price_id, seat_id) 
                    VALUES (@ReservationId, @SeatPriceId, @SeatId)
                ";

                var SeatPrices = new[]
                {
                    new {ReservationId = 1, SeatPriceId = 1, SeatId = 7},
                    new {ReservationId = 1, SeatPriceId = 1, SeatId = 8},
                    new {ReservationId = 1, SeatPriceId = 1, SeatId = 10},
                };

                connection.Execute(sql, SeatPrices);
            }
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS ticket;";
            connection.Execute(sql);
        }
    }
}