using Dapper;

public static class TicketAccess
{
    public static List<Ticket> GetTicketsByMovieSessionId(int movieSessionId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                        ticket.id as Id,
                        reservation_id as ReservationId,
                        seat_price_id as SeatPriceId,
                        seat_id as SeatId
                    FROM ticket
                    JOIN reservation ON ticket.reservation_id = reservation.id
                    WHERE reservation.movie_session_id = @MovieSessionId;";
            return connection.Query<Ticket>(sql, new { MovieSessionId = movieSessionId }).ToList();
        }
        
    }
}
