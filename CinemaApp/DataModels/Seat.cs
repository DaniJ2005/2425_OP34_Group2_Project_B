public class Seat
{
    public int Id { get; set; }
    public int SeatTypeId { get; set; }
    public int MovieHallId { get; set; }
    public char Row { get; set; }
    public int Number { get; set; }
    public bool IsUnderMaintenance { get; set; }
}