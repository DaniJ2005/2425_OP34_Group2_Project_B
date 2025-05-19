using Dapper;


public static class SeatAccess
{
    public static List<Seat> GetAllSeats()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                seat.id AS Id,
                seat_type_id AS SeatTypeId,
                movie_hall_id AS MovieHallId,
                row AS Row,
                col AS Col,
                is_under_maintenance AS IsUnderMaintenance,
                type AS Type,
                color AS Color
            FROM seat
            INNER JOIN seat_type
            ON seat.seat_type_id = seat_type.id";
            return connection.Query<Seat>(sql).ToList();
        }
    }

    public static Seat GetSeatById(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                seat.id AS Id,
                seat_type_id AS SeatTypeId,
                movie_hall_id AS MovieHallId,
                row AS Row,
                col AS Col,
                is_under_maintenance AS IsUnderMaintenance,
                type AS Type,
                color AS Color
            FROM seat
            INNER JOIN seat_type
            ON seat.seat_type_id = seat_type.id
            WHERE seat.id = @Id";
            return connection.QueryFirstOrDefault<Seat>(sql, new { Id = id });
        }
    }

    public static List<Seat> GetAllByMovieHallId(int movieHallId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT
                seat.id AS Id,
                seat_type_id AS SeatTypeId,
                movie_hall_id AS MovieHallId,
                row AS Row,
                col AS Col,
                is_under_maintenance AS IsUnderMaintenance,
                type AS Type,
                color AS Color
            FROM seat
            INNER JOIN seat_type
            ON seat.seat_type_id = seat_type.id
            WHERE movie_hall_id = @MovieHallId";
            return connection.Query<Seat>(sql, new { MovieHallId = movieHallId }).ToList();
        }
    }

    public static void AddSeat(Seat seat)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"INSERT INTO seat 
                (seat_type_id, movie_hall_id, row, col, is_under_maintenance)
                VALUES 
                (@SeatTypeId, @MovieHallId, @Row, @Col, @IsUnderMaintenance);
                SELECT LAST_INSERT_ID()";
            
            int id = connection.ExecuteScalar<int>(sql, new { 
                seat.SeatTypeId, 
                seat.MovieHallId, 
                seat.Row, 
                seat.Col, 
                seat.IsUnderMaintenance 
            });
            
            seat.Id = id;
        }
    }

    public static void UpdateSeat(Seat seat)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"UPDATE seat 
                SET seat_type_id = @SeatTypeId,
                    movie_hall_id = @MovieHallId,
                    row = @Row,
                    col = @Col,
                    is_under_maintenance = @IsUnderMaintenance
                WHERE id = @Id";
            
            connection.Execute(sql, new { 
                seat.SeatTypeId, 
                seat.MovieHallId, 
                seat.Row, 
                seat.Col, 
                seat.IsUnderMaintenance,
                seat.Id 
            });
        }
    }

    public static void DeleteSeat(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "DELETE FROM seat WHERE id = @Id";
            connection.Execute(sql, new { Id = id });
        }
    }
}