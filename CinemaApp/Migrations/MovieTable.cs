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
                    new { Title = "A Minecraft Movie", Description = minecraftMovieDesc, Genre = "Actie/Avontuur/Familiefilm", Duration = "1h 41m", Language = "EN", MinAge = 9},
                    new { Title = "Dune: Part Two", Description = "Paul Atreides unites with the Fremen.", Genre = "Sci-Fi/Actie", Duration = "2h 46m", Language = "EN", MinAge = 12},
                    new { Title = "Wonka", Description = "Origin story of Willy Wonka.", Genre = "Familie/Fantasie", Duration = "1h 57m", Language = "EN", MinAge = 6},
                    new { Title = "The Marvels", Description = "Marvel superheroes team up.", Genre = "Actie/Fantasie", Duration = "1h 45m", Language = "EN", MinAge = 12},
                    new { Title = "Wish", Description = "Asha makes a powerful wish.", Genre = "Animatie/Familie", Duration = "1h 35m", Language = "EN", MinAge = 6},
                    new { Title = "The Hunger Games: The Ballad of Songbirds and Snakes", Description = "Prequel to the Hunger Games saga.", Genre = "Actie/Drama", Duration = "2h 38m", Language = "EN", MinAge = 12},
                    new { Title = "Oppenheimer", Description = "Story of J. Robert Oppenheimer.", Genre = "Drama/Historisch", Duration = "3h 0m", Language = "EN", MinAge = 16},
                    new { Title = "Barbie", Description = "Barbie explores the real world.", Genre = "Komedie/Fantasie", Duration = "1h 54m", Language = "EN", MinAge = 9},
                    new { Title = "Spider-Man: Across the Spider-Verse", Description = "Miles Morales returns.", Genre = "Animatie/Actie", Duration = "2h 20m", Language = "EN", MinAge = 9},
                    new { Title = "The Super Mario Bros. Movie", Description = "Mario's animated adventure.", Genre = "Animatie/Actie", Duration = "1h 32m", Language = "EN", MinAge = 6},
                    new { Title = "Napoleon", Description = "Epic tale of Napoleon Bonaparte.", Genre = "Historisch/Drama", Duration = "2h 38m", Language = "EN", MinAge = 16},
                    new { Title = "Elemental", Description = "Fire and water find harmony.", Genre = "Animatie/Fantasie", Duration = "1h 42m", Language = "EN", MinAge = 6},
                    new { Title = "Trolls Band Together", Description = "Trolls go on a musical journey.", Genre = "Animatie/Familie", Duration = "1h 32m", Language = "EN", MinAge = 6},
                    new { Title = "Aquaman and the Lost Kingdom", Description = "Aquaman faces new threats.", Genre = "Actie/Fantasie", Duration = "2h 4m", Language = "EN", MinAge = 12},
                    new { Title = "Killers of the Flower Moon", Description = "Crime saga of oil and betrayal.", Genre = "Drama/Thriller", Duration = "3h 26m", Language = "EN", MinAge = 16},
                    new { Title = "Migration", Description = "A family of ducks migrates.", Genre = "Animatie/Familie", Duration = "1h 22m", Language = "EN", MinAge = 6},
                    new { Title = "Kung Fu Panda 4", Description = "Po returns for another adventure.", Genre = "Animatie/Actie", Duration = "1h 34m", Language = "EN", MinAge = 6},
                    new { Title = "Inside Out 2", Description = "New emotions arrive.", Genre = "Animatie/Familie", Duration = "1h 40m", Language = "EN", MinAge = 6},
                    new { Title = "Ghostbusters: Frozen Empire", Description = "Ghostbusters face a chilling threat.", Genre = "Actie/Komedie", Duration = "1h 55m", Language = "EN", MinAge = 12},
                    new { Title = "IF (Imaginary Friends)", Description = "A girl sees everyone's imaginary friends.", Genre = "Fantasie/Komedie", Duration = "1h 44m", Language = "EN", MinAge = 6},
                    new { Title = "Godzilla x Kong: The New Empire", Description = "Monsters clash in an epic battle.", Genre = "Actie/Fantasie", Duration = "1h 55m", Language = "EN", MinAge = 12}
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