using Dapper;

public static class AccountsAccess
{
    private static DatabaseContext _dbContext = new DatabaseContext();
    private static string Table = "Accounts";

    public static void Write(AccountModel account)
    {
        using (var connection = _dbContext.CreateConnection())
        {
            string sql = $"INSERT INTO {Table} (email, password, fullname) VALUES (@EmailAddress, @Password, @FullName)";
            connection.Execute(sql, account);  
        }
    }

    public static AccountModel GetByEmail(string email)
    {
        using (var connection = _dbContext.CreateConnection())
        {
            string sql = $"SELECT * FROM {Table} WHERE email = @Email";
            return connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
        }
    }

    public static List<AccountModel> GetAllAccounts()
    {
        using (var connection = _dbContext.CreateConnection())
        {
            string sql = $"SELECT * FROM Accounts;";
            var accounts = connection.Query<AccountModel>(sql).ToList();
            return accounts; 
        }
    }

    public static void Update(AccountModel account)
    {
        using (var connection = _dbContext.CreateConnection())
        {
            string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
            connection.Execute(sql, account);
        }
    }

    public static void Delete(AccountModel account)
    {
        using (var connection = _dbContext.CreateConnection())
        {
            string sql = $"DELETE FROM {Table} WHERE id = @Id";
            connection.Execute(sql, new { Id = account.Id });
        }
    }
}
