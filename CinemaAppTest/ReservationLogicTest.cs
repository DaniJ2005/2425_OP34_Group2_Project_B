[TestClass]
public class ReservationLogicTest
{
    private Movie validMovie;
    private MovieSession validSession;
    private Dictionary<Seat, SeatPrice> sampleSeats;
    private Dictionary<Food, int> sampleFoodItems;

    [TestInitialize]
    public void Setup()
    {
        validMovie = new Movie
        {
            Id = 1,
            Title = "Test Movie",
            Duration = "02:00"
        };

        validSession = new MovieSession
        {
            Id = 1,
            Date = "2025-06-17",
            StartTime = "20:00",
            MovieId = 1,
            MovieHallId = 3
        };

        sampleSeats = new Dictionary<Seat, SeatPrice>
        {
            [new Seat { Row = 1, Col = 5 }] = new SeatPrice { Price = 9.99 }
        };

        sampleFoodItems = new Dictionary<Food, int>
        {
            [new Food { Name = "Nachos" }] = 1
        };
    }

    [TestMethod]
    public void GetConfirmationSummary_WhenMovieNotSet_ReturnsFallback()
    {
        // Arrange: Only movie is missing
        ReservationLogic.ClearSelection();
        ReservationLogic.SetSelectedSession(validSession);

        // Act
        string result = ReservationLogic.GetConfirmationSummary();

        // Assert
        Assert.AreEqual("No reservation selected.", result);
    }

    [TestMethod]
    public void GetConfirmationSummary_WhenMovieAndSessionAreSet_ReturnsFormattedSummary()
    {
        // Arrange
        ReservationLogic.SetSelectedMovie(validMovie);
        ReservationLogic.SetSelectedSession(validSession);

        // Act
        string result = ReservationLogic.GetConfirmationSummary();

        // Assert
        Assert.IsTrue(result.Contains(validMovie.Title));
        Assert.IsTrue(result.Contains(validSession.Date));
        Assert.IsTrue(result.Contains(validSession.MovieHallId.ToString()), "Hall info missing.");
    }

    [TestMethod]
    public void ClearSelection_ClearsAllTemporaryReservationData()
    {
        // Arrange
        ReservationLogic.SetSelectedMovie(validMovie);
        ReservationLogic.SetSelectedSession(validSession);
        ReservationLogic.SetSelectedSeats(sampleSeats);
        ReservationLogic.SetSelectedFoodItems(sampleFoodItems);

        // Act
        ReservationLogic.ClearSelection();

        // Assert
        Assert.IsNull(ReservationLogic.GetSelectedMovie());
        Assert.IsNull(ReservationLogic.GetSelectedSession());
        Assert.AreEqual(0, ReservationLogic.GetSelectedSeats().Count);
        Assert.AreEqual(0, ReservationLogic.GetSelectedFoodItems().Count);
    }

    [DataTestMethod]
    [DataRow(0)] // Empty food dictionary
    [DataRow(2)] // Multiple food items
    public void SetSelectedFoodItems_TracksCorrectFoodCounts(int foodCount)
    {
        // Arrange
        var foodItems = new Dictionary<Food, int>();
        for (int i = 0; i < foodCount; i++)
        {
            foodItems.Add(new Food { Id = i + 1, Name = $"Item{i + 1}" }, i + 1);
        }

        // Act
        ReservationLogic.SetSelectedFoodItems(foodItems);
        var result = ReservationLogic.GetSelectedFoodItems();

        // Assert
        Assert.AreEqual(foodCount, result.Count);
    }

    [TestMethod]
    public void SetSelectedSeats_WithExternalDictionary_DoesNotModifyOriginalReference()
    {
        // Arrange
        var original = new Dictionary<Seat, SeatPrice>(sampleSeats);

        // Act
        ReservationLogic.SetSelectedSeats(original);
        original.Clear(); // try to mutate original

        // Assert
        Assert.AreNotEqual(0, ReservationLogic.GetSelectedSeats().Count,
            "ReservationLogic should copy the dictionary, not reference it.");
    }

    [TestMethod]
    public void ClearIndividualComponents_DoesNotAffectOthers()
    {
        // Arrange
        ReservationLogic.SetSelectedMovie(validMovie);
        ReservationLogic.SetSelectedSession(validSession);
        ReservationLogic.SetSelectedSeats(sampleSeats);
        ReservationLogic.SetSelectedFoodItems(sampleFoodItems);

        // Act
        ReservationLogic.ClearSeats();
        ReservationLogic.ClearMovie();

        // Assert
        Assert.IsNull(ReservationLogic.GetSelectedMovie());
        Assert.IsNotNull(ReservationLogic.GetSelectedSession());
        Assert.AreEqual(0, ReservationLogic.GetSelectedSeats().Count);
        Assert.AreNotEqual(0, ReservationLogic.GetSelectedFoodItems().Count);
    }
}
