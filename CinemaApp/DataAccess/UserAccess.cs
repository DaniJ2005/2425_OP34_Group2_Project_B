using Dapper;

public static class UserAccess
{
    public static void Write(UserModel user)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "INSERT INTO user (email, password, username) VALUES (@Email, @Password, @UserName)";
            connection.Execute(sql, user);
        }
    }

    public static UserModel GetByEmail(string email)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM user WHERE email = @Email";
            return connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email });
        }
    }

    public static List<UserModel> GetAllUsers()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM User;";
            return connection.Query<UserModel>(sql).ToList();
        }
    }

    public static void Delete(UserModel account)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "DELETE FROM user WHERE id = @Id";
            connection.Execute(sql, new { Id = account.Id });
        }
    }

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

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS user;";
            connection.Execute(sql);
        }
    }
}
