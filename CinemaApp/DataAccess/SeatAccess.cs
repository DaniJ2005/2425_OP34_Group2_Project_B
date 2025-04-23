using Dapper;

public static class SeatAccess
{
    public static List<SeatModel> GetSeatsByHallId(int hallId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT * FROM Seat 
                WHERE MovieHallId = @HallId
                ORDER BY Row, Number";
            
            return connection.Query<SeatModel>(sql, new { HallId = hallId }).ToList();
        }
    }

    public static List<SeatModel> GetReservedSeatsBySessionId(int sessionId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT s.* FROM Seat s
                JOIN Reservation r ON s.Id = r.SeatId
                WHERE r.SessionId = @SessionId";
                
            return connection.Query<SeatModel>(sql, new { SessionId = sessionId }).ToList();
        }
    }
    
    public static SeatModel GetSeatById(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM Seat WHERE Id = @Id";
            return connection.QueryFirstOrDefault<SeatModel>(sql, new { Id = id });
        }
    }
    
    public static SeatModel GetSeatByPosition(int hallId, char row, int number)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM Seat WHERE MovieHallId = @HallId AND Row = @Row AND Number = @Number";
            return connection.QueryFirstOrDefault<SeatModel>(sql, 
                new { HallId = hallId, Row = row, Number = number });
        }
    }
}