using Microsoft.Data.Sqlite;
using System;
using System.IO;

public static class Db
{
    private static readonly string _connectionString = GetDatabasePath();

    private static string GetDatabasePath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string dbPath = Path.Combine(baseDirectory, @"..\..\..\DataSources\project.db");

        return Path.GetFullPath(dbPath);
    }

    public static SqliteConnection CreateConnection()
    {
        return new SqliteConnection($"Data Source={_connectionString}");
    }

    public static void InitTables()
    {
        RoleAcccess.InitTable();
        UserAccess.InitTable();
    }

    public static void DeleteTables()
    {
        RoleAcccess.DeleteTable();
        UserAccess.DeleteTable();
    }
}
