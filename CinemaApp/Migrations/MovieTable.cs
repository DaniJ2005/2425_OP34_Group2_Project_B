using Dapper;
public static class MovieTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS movie (
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

    public static void PopulateTable()
    {
        using (var connection = Db.CreateConnection())
        {
            // Check if the table is empty
            string checkSql = "SELECT COUNT(*) FROM movie;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO movie (title, description, genre, duration, language, min_age) 
                    VALUES (@Title, @Description, @Genre, @Duration, @Language, @MinAge)
                ";

                string minecraftMovieDesc = @"Welkom in de wereld van Minecraft, waar creativiteit niet alleen van pas komt, maar van essentieel belang is om te overleven. Vier buitenbeentjes met ieder hun eigen problemen - Garrett “de Vuilnisman” Garrison (Momoa), Henry (Hansen), Natalie (Myers) en Dawn (Brooks) - belanden plotseling via een mysterieus portaal in de Overworld: een bizar kubistisch wonderland...";

                var Movies = new[]
                {
                    new { Title = "A Minecraft Movie", Description = minecraftMovieDesc, Genre = "Actie/Avontuur/Familiefilm", Duration = "02:00", Language = "EN", MinAge = 9},
                    new { Title = "Dune: Part Two", Description = "Paul Atreides unites with the Fremen.", Genre = "Sci-Fi/Actie", Duration = "01:30", Language = "EN", MinAge = 12},
                    
                };

                connection.Execute(sql, Movies);
            }
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