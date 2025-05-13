public class ReservationFood
{
    public int Id { get; set; }
    public int Reservationid { get; set; }
    public int FoodId { get; set; }
    public int Quantity { get; set; }

    // Food Join
    public string Name { get; set; }
    public double Price { get; set; }
    public bool Is_Available { get; set; }
}