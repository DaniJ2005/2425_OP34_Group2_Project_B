using Dapper;
public static class ReservationFoodTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS reservation_food (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    reservation_id INTEGER,
                    food_id INTEGER,
                    quantity INTEGER NOT NULL,
                    FOREIGN KEY (reservation_id) REFERENCES reservation(id),
                    FOREIGN KEY (food_id) REFERENCES food(id)
                );
            ";

            connection.Execute(sql);
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS reservation_food;";
            connection.Execute(sql);
        }
    }
}