public class Seat
{
    public int Id { get; set; }
    public int SeatTypeId { get; set; }
    public int MovieHallId { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }
    public bool IsUnderMaintenance { get; set; }
}