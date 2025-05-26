public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public string Language { get; set; }

    private int _minAge;
    public string MinAge
    {
        get => _minAge + "+";
        set => _minAge = int.Parse(value);
    }
}
