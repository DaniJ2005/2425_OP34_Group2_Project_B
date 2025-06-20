using Dapper;

public static class ReservationFoodAccess
{
    public static List<ReservationFood> GetFoodReservations(int reservationId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT
                            reservation_food.id as Id,
                            reservation_id as ReservationId,
                            food_id as FoodId,
                            quantity as Quantity,
                            food.name as Name,
                            food.price as Price
                        FROM reservation_food
                        INNER JOIN food on reservation_food.food_id = food.id
                        WHERE reservation_id = @ReservationId";
            return connection.Query<ReservationFood>(sql, new { ReservationId = reservationId }).ToList();
        }
    }
}