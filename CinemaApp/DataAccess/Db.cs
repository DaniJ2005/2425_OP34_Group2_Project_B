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
        RoleTable.InitTable();
        UserTable.InitTable();
        MovieHallTable.InitTable();
        MovieTable.InitTable();
        MovieSessionTable.InitTable();
        ReservationTable.InitTable();
        SeatTypeTable.InitTable();
        SeatTable.InitTable();
        SeatPriceTable.InitTable();
        TicketTable.InitTable();
        FoodTable.InitTable();
        ReservationFoodTable.InitTable();
    }

    public static void PopulateTables()
    {
        RoleTable.PopulateTable();
        SeatTypeTable.PopulateTable();
        SeatPriceTable.PopulateTable();
        MovieHallTable.PopulateTable();
        SeatTable.PopulateTable();
        MovieTable.PopulateTable();
        MovieSessionTable.PopulateTable();
    }

    public static void DeleteTables()
    {
        RoleTable.DeleteTable();
        UserTable.DeleteTable();
        SeatPriceTable.DeleteTable();
        SeatTable.DeleteTable();
        SeatTypeTable.DeleteTable();
        MovieSessionTable.DeleteTable();
        MovieHallTable.DeleteTable();
        MovieTable.DeleteTable();
        ReservationTable.DeleteTable();
        TicketTable.DeleteTable();
        FoodTable.DeleteTable();
        ReservationFoodTable.DeleteTable();
    }
}
