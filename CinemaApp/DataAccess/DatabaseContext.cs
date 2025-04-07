using Microsoft.Data.Sqlite;

public class DatabaseContext
{
    private readonly string _connectionString;

    public DatabaseContext()
    {
        _connectionString = GetDatabasePath();
    }

    private string GetDatabasePath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string dbPath = Path.Combine(baseDirectory, @"..\..\..\DataSources\project.db");
        
        return Path.GetFullPath(dbPath);
    }

    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection($"Data Source={_connectionString}");
    }
}
