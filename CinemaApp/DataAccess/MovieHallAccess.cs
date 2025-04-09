using Dapper;

public static class MovieHallAccess
{

    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE movie_hall (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL
                );
            ";

            connection.Execute(sql);
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS movie_hall;";
            connection.Execute(sql);
        }
    }
}
