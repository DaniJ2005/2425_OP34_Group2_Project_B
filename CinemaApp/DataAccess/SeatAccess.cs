using Dapper;

public static class SeatAccess
{
    public static List<Seat> GetAllByMovieHallId(int movieHallId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                            id AS Id,
                            seat_type_id AS SeatTypeId,
                            movie_hall_id AS MovieHallId,
                            row AS Row,
                            col AS Col,
                            is_under_maintenance AS IsUnderMaintenance
                        FROM seat WHERE movie_hall_id = @MovieHallId";
            return connection.Query<Seat>(sql, new { MovieHallId = movieHallId }).ToList();
        }
    }
}
