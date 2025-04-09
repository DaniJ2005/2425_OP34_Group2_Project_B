using Dapper;

public static class MovieAccess
{

    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE movie (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    description TEXT,
                    genre TEXT,
                    duration TEXT,
                    language TEXT,
                    min_age INTEGER
                );
            ";

            connection.Execute(sql);
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS movie;";
            connection.Execute(sql);
        }
    }
}
