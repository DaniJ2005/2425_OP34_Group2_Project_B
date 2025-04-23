public class MovieHallModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int[,] SeatLayout { get; set; }
}