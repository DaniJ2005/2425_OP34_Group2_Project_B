public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieSessionId { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string CreatedAt { get; set; }
    public double TotalPrice { get; set; }
    public string TotalPriceString => $"€{TotalPrice:F2}";

    public string MovieHall { get; set; }
    public string MovieTitle { get; set; }
    public string StartTime { get; set; }
    public string Date { get; set; }
}