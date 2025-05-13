using Dapper;

public static class FoodAccess
{
    public static List<Food> GetAllFood()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM Food;";
            return connection.Query<Food>(sql).ToList();
        }
    }
}
