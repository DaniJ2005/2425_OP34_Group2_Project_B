public class MovieSession
{
    public int Id { get; set; }
    public int MovieHallId { get; set; }
    public int MovieId { get; set; }
    public string StartTime { get; set; }
    public string Date { get; set; }

    // INNER JOIN ticket -> Movie
    public string MovieTitle { get; set; }
    // INNER JOIN ticket -> MovieHall
    public string MovieHallName { get; set; }
}