using Dapper;
public static class UserTable
{

    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS user (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    role_id INTEGER,
                    username TEXT NOT NULL,
                    email TEXT UNIQUE NOT NULL,
                    password TEXT NOT NULL,
                    FOREIGN KEY (role_id) REFERENCES role(id)
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
            string checkSql = "SELECT COUNT(*) FROM user;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO user (role_id, username, email, password)
                    VALUES (@RoleId, @Username, @Email, @Password);
                ";

                var defaultUser = new
                {
                    RoleId = 1,
                    Username = "admin",
                    Email = "admin@admin.com",
                    Password = CryptoHelper.Hash("12345678")
                };

                connection.Execute(sql, defaultUser);
            }
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS user;";
            connection.Execute(sql);
        }
    }
}