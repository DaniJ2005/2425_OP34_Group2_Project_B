public class RoleModel
{
    public int Id { get; set; }
    public bool ManageFoodMenu { get; set; }
    public bool ManageAccounts { get; set; }
    public bool ManageGuestAccounts { get; set; }
    public bool ManageMovieSessions { get; set; }
    public bool ManageMovieHall { get; set; }
    public bool ManageReservations { get; set; }
}