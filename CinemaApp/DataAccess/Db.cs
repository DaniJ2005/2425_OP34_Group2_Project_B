using Microsoft.Data.Sqlite;
using System;
using System.IO;

public static class Db
{
    private static readonly string _connectionString = GetDatabasePath();

    private static string GetDatabasePath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string dbPath = Path.Combine(baseDirectory, @"..\..\..\DataSources\DB\project.db");

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

        SeatTypeTable.InitTable();
        MovieHallTable.InitTable();
        SeatTable.InitTable();
        SeatPriceTable.InitTable();

        MovieTable.InitTable();
        MovieSessionTable.InitTable();

        ReservationTable.InitTable();
        TicketTable.InitTable();

        FoodTable.InitTable();
        ReservationFoodTable.InitTable();
    }

    public static void PopulateTables()
    {
        RoleTable.PopulateTable();         // needed for User
       // needed for Reservation

        SeatTypeTable.PopulateTable();     // needed for Seat & SeatPrice
        MovieHallTable.PopulateTable();    // needed for Seat & MovieSession

        SeatTable.PopulateTable();         // needs SeatType and MovieHall
        SeatPriceTable.PopulateTable();    // needs SeatType

        MovieTable.PopulateTable();        // needed for MovieSession
        MovieSessionTable.PopulateTable(); // needs Movie and MovieHall

        ReservationTable.PopulateTable();  // needs User and MovieSession

        TicketTable.PopulateTable();       // needs Reservation, Seat, SeatPrice
    }

    public static void DeleteTables()
    {
        // Delete child tables first
        ReservationFoodTable.DeleteTable();
        TicketTable.DeleteTable();
        ReservationTable.DeleteTable();
        //UserTable.DeleteTable();     // vanaf nu deze niet meer deleten?
        
        SeatPriceTable.DeleteTable();
        SeatTable.DeleteTable();
        SeatTypeTable.DeleteTable();

        MovieSessionTable.DeleteTable();
        MovieTable.DeleteTable();
        MovieHallTable.DeleteTable();
        
        FoodTable.DeleteTable();
        RoleTable.DeleteTable();
    }
}
