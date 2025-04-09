using Dapper;

public static class SeatPriceAccess
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

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS seat_price;";
            connection.Execute(sql);
        }
    }
}
