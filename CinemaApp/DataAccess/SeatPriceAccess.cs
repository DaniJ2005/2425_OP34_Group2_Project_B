using Dapper;

public static class SeatPriceAccess
{
    public static List<SeatPrice> GetAllSeatPricesWithoutPromo()
    {
        string noPromo = "none";
        using (var connection = Db.CreateConnection())
        {
            string sql = @"SELECT id as Id, seat_type_id as SeatTypeId, promo as Promo, price as Price FROM seat_price WHERE promo = @Promo";
            return connection.Query<SeatPrice>(sql, new {Promo = noPromo}).ToList();
        }
        
    }
    
}
