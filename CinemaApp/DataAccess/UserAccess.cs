using Dapper;

public static class UserAccess
{
    public static void Write(User user)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "INSERT INTO user (email, password, username) VALUES (@Email, @Password, @UserName)";
            connection.Execute(sql, user);
        }
    }

    public static User GetByEmail(string email)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM user WHERE email = @Email";
            return connection.QueryFirstOrDefault<User>(sql, new { Email = email });
        }
    }

    public static List<User> GetAllUsers()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM User;";
            return connection.Query<User>(sql).ToList();
        }
    }

    public static void Delete(User account)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "DELETE FROM user WHERE id = @Id";
            connection.Execute(sql, new { Id = account.Id });
        }
    }
}
