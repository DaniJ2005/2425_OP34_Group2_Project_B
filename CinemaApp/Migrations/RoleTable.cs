using Dapper;
public static class RoleTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS role (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
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

    public static void PopulateTable()
    {
        using (var connection = Db.CreateConnection())
        {
            // Check if the table is empty
            string checkSql = "SELECT COUNT(*) FROM role;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO role (name, manage_food_menu, manage_accounts, manage_guest_accounts, manage_movie_sessions, manage_movie_hall, manage_reservations) 
                    VALUES (@Name, @ManageFoodMenu, @ManageAccounts, @ManageGuestAccounts, @ManageMovieSessions, @ManageMovieHall, @ManageReservations);
                ";

                var Roles = new[]
                {
                    new { Name = "Admin", ManageFoodMenu = true, ManageAccounts = true, ManageGuestAccounts = true, ManageMovieSessions = true, ManageMovieHall = true,  ManageReservations = true},
                };

                connection.Execute(sql, Roles);
            }
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