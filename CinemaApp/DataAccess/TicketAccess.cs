using Dapper;

public static class TicketAccess
{

    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE ticket (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    seat_id INTEGER,
                    reservation_id INTEGER,
                    promotion_id INTEGER,
                    processed_price REAL NOT NULL,
                    FOREIGN KEY (seat_id) REFERENCES seat(id),
                    FOREIGN KEY (reservation_id) REFERENCES reservation(id),
                    FOREIGN KEY (promotion_id) REFERENCES promotion(id)
                );
            ";

            connection.Execute(sql);
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
