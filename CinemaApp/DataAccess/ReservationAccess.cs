using System.Net.Quic;
using Dapper;

public static class ReservationAccess
{
    public static void CreateReservation(MovieSession movieSession, Dictionary<Seat, SeatPrice> seats, Dictionary<Food, int> foodItems, string email, User currentUser, double totalPrice)
    {
        using (var connection = Db.CreateConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Insert reservation & retrieve auto generated Id
                    string reservationQuery = @"
                        INSERT INTO reservation (user_id, movie_session_id, email, total_price, status) 
                        VALUES (@UserId, @MovieSessionId, @Email, @TotalPrice, @Status); 
                        SELECT last_insert_rowid();";

                    var parameters = new
                    {
                        UserId = currentUser?.Id,  // Will be null if currentUser is null
                        MovieSessionId = movieSession.Id,
                        Email = email,
                        TotalPrice = totalPrice,
                        Status = "Confirmed"
                    };

                    long reservationId = connection.ExecuteScalar<long>(
                        reservationQuery,
                        parameters,
                        transaction
                    );

                    // Insert tickets
                    string ticketQuery = "INSERT INTO ticket (reservation_id, seat_id, seat_price_id) VALUES (@ReservationId, @SeatId, @SeatPriceId)";

                    foreach (var seat in seats)
                    {
                        connection.Execute(
                            ticketQuery,
                            new
                            {
                                ReservationId = reservationId,
                                SeatPriceId = seat.Value.Id,
                                SeatId = seat.Key.Id
                            },
                            transaction
                        );
                    }

                    // Insert Food items
                    string foodQuery = "INSERT INTO reservation_food (reservation_id, food_id, quantity) VALUES (@ReservationId, @FoodId, @Quantity)";

                    foreach (var foodItem in foodItems)
                    {
                        connection.Execute(
                            foodQuery,
                            new
                            {
                                ReservationId = reservationId,
                                FoodId = foodItem.Key.Id,
                                Quantity = foodItem.Value
                            },
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

    public static List<Reservation> GetReservationsByUserId(int userId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT 
                    reservation.id AS Id,
                    status AS Status,
                    total_price AS TotalPrice,
                    created_at AS CreatedAt,
                    movie_session.date AS Date,
                    movie_session.start_time AS StartTime,
                    movie.title AS MovieTitle,
                    movie_hall.name AS MovieHall
                FROM reservation
                INNER JOIN movie_session 
                    ON reservation.movie_session_id = movie_session.id
                INNER JOIN movie 
                    ON movie_session.movie_id = movie.id
                INNER JOIN movie_hall
                    ON movie_session.movie_hall_id = movie_hall.id
                WHERE user_id = 1;
            ";
            return connection.Query<Reservation>(sql, new { UserId = userId }).ToList();
        }
    }

}
