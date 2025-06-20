public class SeatPrice
{
    public int Id { get; set; }
    public int SeatTypeId { get; set; }
    public string Promo { get; set; }
    public double Price { get; set; }

    // INNER JOIN seat_price -> seat_type
    
    public string Type { get; set; }
    public string Color { get; set; }
    
}