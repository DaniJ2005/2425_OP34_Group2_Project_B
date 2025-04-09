using Dapper;

public static class SeatTypeAccess
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

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS seat_type;";
            connection.Execute(sql);
        }
    }
}
