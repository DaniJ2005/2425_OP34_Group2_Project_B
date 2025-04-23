using Dapper;

public static class SeatAccess
{
    public static List<SeatModel> SeatsForMovie(MovieModel selectedMovie)
    {
        using (var conn = Db.CreateConnection())
        {
            string sql = @"
               SELECT s.*                            -- Get all columns from seat table
                FROM seat s
               INNER JOIN movie_hall mh ON s.movie_hall_id = mh.id      -- Join seat to movie hall
               INNER JOIN movie_session ms ON ms.movie_hall_id = mh.id  -- Join movie hall to movie session
               WHERE ms.movie_id = @MovieId;         -- Only include rows where the movie is the selected one";

           return conn.Query<SeatModel>(sql, new { MovieId = selectedMovie.Id }).ToList();
        }
    }

    //query schrijven die juiste data ophaalt   method GetSeatsByMovieHallID
    // reservationlogic.getselectedmovie
}
