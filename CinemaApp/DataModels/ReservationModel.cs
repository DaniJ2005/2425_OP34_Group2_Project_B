public class ReservationModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieSessionId { get; set; }
    public string Status { get; set; }
    public string CreatedAt { get; set; }
}