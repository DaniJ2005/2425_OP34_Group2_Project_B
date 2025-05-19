using Dapper;
public static class MovieHallTable
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

    public static void PopulateTable()
    {
        using (var connection = Db.CreateConnection())
        {
            // Check if the table is empty
            string checkSql = "SELECT COUNT(*) FROM movie_hall;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO movie_hall (name) 
                    VALUES (@Name)
                ";

                var MovieHalls = new[]
                {
                    new { Name = "Hall01" },
                    new { Name = "Hall02" }
                };

                connection.Execute(sql, MovieHalls);
            }
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