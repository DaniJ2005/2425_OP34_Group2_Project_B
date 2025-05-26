public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public string Language { get; set; }

    public int MinAge { get; set; }

    // Read-only property for UI purposes
    public string MinAgeDisplay => $"{MinAge}+";
}
