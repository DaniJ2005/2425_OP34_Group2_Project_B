using Dapper;
public static class SeatTypeTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE seat_type (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    seat_type TEXT
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
            string checkSql = "SELECT COUNT(*) FROM seat_type;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO seat_type (seat_type)
                    VALUES (@SeatType)
                ";

                var seatTypes = new[]
                {
                    new { SeatType = "Regular" },
                    new { SeatType = "VIP" }
                };

                connection.Execute(sql, seatTypes);
            }
        }
    }


    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS seat_type;";
            connection.Execute(sql);
        }
    }
}