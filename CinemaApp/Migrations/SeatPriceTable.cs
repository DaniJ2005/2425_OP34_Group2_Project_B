using Dapper;
public static class SeatPriceTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE seat_price (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    seat_type_id INTEGER,
                    promo TEXT NOT NULL,
                    price REAL NOT NULL,
                    FOREIGN KEY (seat_type_id) REFERENCES seat_type(id)
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
            string checkSql = "SELECT COUNT(*) FROM seat_price;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO seat_price (seat_type_id, promo, price) 
                    VALUES (@SeatTypeId, @Promo, @Price)
                ";

                var SeatPrices = new[]
                {
                    new { SeatTypeId = "1", Promo = "none", Price = 14.25},
                    new { SeatTypeId = "2", Promo = "none", Price = 17.99},
                    new { SeatTypeId = "3", Promo = "none", Price = 25.99},
                };

                connection.Execute(sql, SeatPrices);
            }
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS seat_price;";
            connection.Execute(sql);
        }
    }
}