public class Ticket
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public int ReservationId { get; set; }
    public int SeatPriceId { get; set; }

    // INNER JOIN ticket -> Seat_price
    public string Promo { get; set; }
    public double Price { get; set; }

    // INNER JOIN ticket -> seat
    public int SeatTypeId { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }

    // INNER JOIN ticket -> seat -> seat_type
    public string SeatType { get; set; }
}