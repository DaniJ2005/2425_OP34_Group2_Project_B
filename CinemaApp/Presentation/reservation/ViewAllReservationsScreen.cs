public class ViewAllReservationsScreen : IScreen
{
    public string ScreenName { get; set; }
    private List<Reservation> _reservations = [];
    public ViewAllReservationsScreen() => ScreenName = "Food";

    public void Start()
    {
        ReservationLogic.ClearSelectedReservation();

        _reservations = ReservationLogic.GetAllReservationsForCurrentUser();
        Screen();
    }

    public void Screen()
    {
        Table<Reservation> reservationTable = new(maxColWidth: 40, pageSize: 10);
        reservationTable.SetColumns("MovieTitle", "Date", "StartTime", "MovieHall", "TotalPriceString", "Status");
        reservationTable.AddRows(_reservations);

        ConsoleKey key;

        do
        {
            General.ClearConsole();

            Console.WriteLine("Your Reservations:\n");

            reservationTable.Print("Movie", "Date", "Start Time", "Movie Hall", "TotalPrice", "Status");

            if (_reservations.Count == 0)
            {
                Console.WriteLine("You currently dont have any reservations.");
                Console.WriteLine("Press [ESC] To return.");
            }
        
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                reservationTable.MoveUp();
            if (key == ConsoleKey.DownArrow)
                reservationTable.MoveDown();
            if (key == ConsoleKey.RightArrow)
                reservationTable.NextPage();
            if (key == ConsoleKey.LeftArrow)
                reservationTable.PreviousPage();
            if (key == ConsoleKey.Enter && _reservations.Count != 0)
            {
                Reservation selectedReservation = reservationTable.GetSelected();
                ReservationLogic.SetSelectedReservation(selectedReservation);
                MenuLogic.NavigateTo(new ViewSingleReservationScreen());
            }

            

        } while (key != ConsoleKey.Escape);

        MenuLogic.NavigateToPrevious();
    }
}