using Dapper;

public static class RoleAcccess
{

    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS role (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                manage_food_menu BOOLEAN,
                manage_accounts BOOLEAN,
                manage_guest_accounts BOOLEAN,
                manage_movie_sessions BOOLEAN,
                manage_movie_hall BOOLEAN,
                manage_reservations BOOLEAN
                );
            ";

            connection.Execute(sql);
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS role;";
            connection.Execute(sql);
        }
    }
}
