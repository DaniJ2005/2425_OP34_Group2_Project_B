using Dapper;
public static class FoodTable
{
    public static void InitTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                CREATE TABLE food (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    price REAL NOT NULL,
                    is_available BOOLEAN NOT NULL DEFAULT 1
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
            string checkSql = "SELECT COUNT(*) FROM food;";
            int count = connection.ExecuteScalar<int>(checkSql);

            if (count == 0) // Only insert if no records exist
            {
                string sql = @"
                    INSERT INTO food (id, name, price, is_available)
                    VALUES (@Id, @Name, @Price, @Is_Available)
                ";

                var Foods = new[]
                {
                    new { Id = 1, Name = "Zoete Popcorn Small", Price = 3.50, Is_Available = true },
                    new { Id = 2, Name = "Zoete Popcorn Medium", Price = 6.00, Is_Available = true },
                    new { Id = 3, Name = "Zoete Popcorn Large", Price = 8.50, Is_Available = true },

                    new { Id = 4, Name = "Zoute Popcorn Small", Price = 3.50, Is_Available = true },
                    new { Id = 5, Name = "Zoute Popcorn Medium", Price = 6.00, Is_Available = true },
                    new { Id = 6, Name = "Zoute Popcorn Large", Price = 8.50, Is_Available = true },

                    new { Id = 7, Name = "Zoet & Zout Popcorn Small", Price = 3.50, Is_Available = true },
                    new { Id = 8, Name = "Zoet & Zout Popcorn Medium", Price = 6.00, Is_Available = true },
                    new { Id = 9, Name = "Zoet & Zout Popcorn Large", Price = 8.50, Is_Available = true },

                    new { Id = 10, Name = "Nachos", Price = 4.00, Is_Available = true },

                    new { Id = 11, Name = "M&M's", Price = 5.00, Is_Available = true },

                    new { Id = 12, Name = "Nasischotel", Price = 7.00, Is_Available = true },

                    new { Id = 13, Name = "Nachos", Price = 4.00, Is_Available = true },
                };

                connection.Execute(sql, Foods);
            }
        }
    }

    public static void DeleteTable()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"DROP TABLE IF EXISTS food;";
            connection.Execute(sql);
        }
    }
}
