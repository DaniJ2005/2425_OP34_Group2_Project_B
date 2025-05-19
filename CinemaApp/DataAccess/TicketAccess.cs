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

    public static List<Ticket> GetTicketsByReservationId(int reservationId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                            ticket.id as Id,
                            seat_id as SeatId,
                            reservation_id as ReservationId,
                            seat_price_id as SeatPriceId,
                            seat_price.promo as Promo,
                            seat_price.price as Price,
                            seat.seat_type_id as SeatTypeId,
                            seat.row as Row,
                            seat.col as Col,
                            seat_type.type as SeatType
                        FROM ticket
                        INNER JOIN seat_price ON ticket.seat_price_id = seat_price.id
                        INNER JOIN seat ON ticket.seat_id = seat.id
                        INNER JOIN seat_type ON seat.seat_type_id = seat_type.id
                        WHERE reservation_id = @ReservationId";
            return connection.Query<Ticket>(sql, new { ReservationId = reservationId }).ToList();
        }
        
    }
}
