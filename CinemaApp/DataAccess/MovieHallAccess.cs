using Dapper;
public static class MovieHallAccess
{
    public static List<MovieHallModel> GetAllHalls()
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM movie_hall;";
            var rawHalls = connection.Query<MovieHallRaw>(sql).ToList();
            return rawHalls.Select(ConvertToModel).ToList();
        }
    }

    public static MovieHallModel GetHallById(int id)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = "SELECT * FROM movie_hall WHERE Id = @Id;";
            var raw = connection.QueryFirstOrDefault<MovieHallRaw>(sql, new { Id = id });
            return raw != null ? ConvertToModel(raw) : null;
        }
    }

    private static MovieHallModel ConvertToModel(MovieHallRaw raw)
    {
        return new MovieHallModel
        {
            Id = raw.Id,
            Name = raw.Name,
            Rows = raw.Rows,
            Columns = raw.Columns
        };
    }

    private class MovieHallRaw
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string SeatLayout { get; set; }
    }
}
