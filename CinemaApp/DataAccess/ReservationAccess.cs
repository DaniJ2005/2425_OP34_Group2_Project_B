using System.Net.Quic;
using Dapper;

public static class ReservationAccess
{
    public static void CreateReservation(MovieSession movieSession, Dictionary<Seat, SeatPrice> seats, List<Food> foodItems, string email)
    {
        using (var connection = Db.CreateConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Insert reservation & retrieve auto generated Id
                    string reservationQuery = "INSERT INTO reservation (movie_session_id, email, status) VALUES (@MovieSessionId, @Email, @Status); SELECT last_insert_rowid();";

                    long reservationId = connection .ExecuteScalar<long>
                    (
                        reservationQuery, 
                        new { MovieSessionId = movieSession.Id, Email = email, Status = "Pending"}, 
                        transaction
                    );

                    // Insert ticket's
                    string ticketQuery = "INSERT INTO ticket (reservation_id, seat_id, seat_price_id) VALUES (@ReservationId, @SeatId, @SeatPriceId)";

                    foreach (var seat in seats)
                    {
                        connection.Execute(
                            ticketQuery,
                            new { ReservationId = reservationId, SeatPriceId = seat.Value.Id, SeatId = seat.Key.Id },
                            transaction
                        );
                    }

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
