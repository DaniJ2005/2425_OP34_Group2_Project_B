using Dapper;

public static class SeatPriceAccess
{
    public static List<SeatPrice> GetAllSeatPricesWithoutPromo()
    {
        string noPromo = "none";
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT id as Id, seat_type_id as SeatTypeId, promo as Promo, price as Price FROM seat_price WHERE promo = @Promo";
            return connection.Query<SeatPrice>(sql, new { Promo = noPromo }).ToList();
        }

    }

    public static List<SeatPrice> GetSeatTypesAndPrices()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT 
                            seat_price.id AS id, 
                            price AS Price,
                            seat_type.type AS Type,
                            seat_type.color AS Color
                        FROM seat_price 
                        INNER JOIN seat_type ON seat_price.seat_type_id = seat_type.id";
            return connection.Query<SeatPrice>(sql).ToList();
        }
    }
    
}
