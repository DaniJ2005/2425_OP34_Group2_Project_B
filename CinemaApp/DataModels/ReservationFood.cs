public class ReservationFood
{
    public int Id { get; set; }
    public int Reservationid { get; set; }
    public int FoodId { get; set; }
    public int Quantity { get; set; }

    // JOIN reservation_food -> food
    public string Name { get; set; }
    public double Price { get; set; }
}
