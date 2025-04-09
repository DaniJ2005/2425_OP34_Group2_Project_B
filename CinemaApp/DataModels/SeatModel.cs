public class SeatModel
{
    public int Id { get; set; }
    public int MovieHallId { get; set; }
    public string SeatType { get; set; }
    public double BasePrice { get; set; }
    public char Row { get; set; }
    public int Number { get; set; }
    public bool IsUnderMaintenance { get; set; }
}