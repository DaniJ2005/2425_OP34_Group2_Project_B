using Dapper;

public static class ReservationAccess
{
    public static void CreateReservation()
    {
        using (var connection = Db.CreateConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertReservationSql = @"
                        INSERT INTO reservation (user_id, movie_session_id, status, created_at)
                        VALUES (@UserId, @MovieSessionId, @Status, @CreatedAt)
                    ";

                    var reservation = new
                    {
                        UserId = UserLogic.CurrentUser.Id,
                        MovieSessionId = 1, // Static test data
                        Status = "Confirmed",
                        CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    connection.Execute(insertReservationSql, reservation, transaction);


                    // Retrieve current reservation id
                    long reservationId = connection.QuerySingle<long>("SELECT last_insert_rowid();", transaction: transaction);

                    string insertTicketSql = @"
                        INSERT INTO ticket (seat_id, reservation_id, seat_price_id) 
                        VALUES (@SeatId, @ReservationId, @SeatPriceId);
                    ";

                    var tickets = new List<object>
                    {
                        new { SeatId = 1, ReservationId = reservationId, SeatPriceId = 1},
                        new { SeatId = 2, ReservationId = reservationId, SeatPriceId = 1},
                        new { SeatId = 3, ReservationId = reservationId, SeatPriceId = 1},
                        new { SeatId = 4, ReservationId = reservationId, SeatPriceId = 1}
                    };

                    connection.Execute(insertTicketSql, tickets, transaction);

                    // Commit transaction
                    transaction.Commit();
                    Console.WriteLine("Reservation and tickets inserted successfully!");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                }
            }
        }
    }
}
