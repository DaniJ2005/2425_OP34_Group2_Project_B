using Dapper;

public static class UserAccess
{
    public static void Write(User user)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "INSERT INTO user (role_id, username, email, password) VALUES (@RoleId, @UserName, @Email, @Password)";
            connection.Execute(sql, user);
        }
    }

    public static void Update(User user)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "UPDATE user SET role_id = @RoleId, username = @UserName, email = @Email, password = @Password WHERE id = @Id";
            connection.Execute(sql, user);
        }
    }

    public static void Delete(User user)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "DELETE FROM user WHERE id = @Id";
            connection.Execute(sql, new { user.Id });
        }
    }

    public static List<User> GetAllUsers()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
            SELECT 
                id AS Id,
                role_id AS RoleId,
                username AS UserName,
                email AS Email,
                password AS Password
            FROM user";
            return connection.Query<User>(sql).ToList();
        }
    }

    public static User GetByEmail(string email)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
            SELECT 
                id AS Id,
                role_id AS RoleId,
                username AS UserName,
                email AS Email,
                password AS Password
            FROM user
            WHERE email = @Email";
            return connection.QueryFirstOrDefault<User>(sql, new { Email = email });
        }
    }
}
