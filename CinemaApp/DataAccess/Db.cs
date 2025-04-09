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
        MovieHallAccess.InitTable();
        MovieAccess.InitTable();
        MovieSessionAccess.InitTable();
        ReservationAccess.InitTable();
        SeatAccess.InitTable();
        PromotionAccess.InitTable();
        TicketAccess.InitTable();
        FoodAccess.InitTable();
        ReservationFoodAccess.InitTable();
    }

    public static void DeleteTables()
    {
        UserAccess.DeleteTable();
        RoleAcccess.DeleteTable();
        MovieHallAccess.DeleteTable();
        MovieAccess.DeleteTable();
        MovieSessionAccess.DeleteTable();
        ReservationAccess.DeleteTable();
        SeatAccess.DeleteTable();
        PromotionAccess.DeleteTable();
        TicketAccess.DeleteTable();
        FoodAccess.DeleteTable();
        ReservationFoodAccess.DeleteTable();
    }
}
