public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public string Language { get; set; }

    // This is what Dapper will bind to/from the DB column 'min_age'
    public int MinAgeDb
    {
        get => _minAge;
        set
        {
            _minAge = value;
            MinAge = _minAge + "+";
        }
    }

    private int _minAge;

    // This is the public-facing string property ("16+")
    public string MinAge
    {
        get => _minAge + "+";
        set => _minAge = int.Parse(value.Replace("+", ""));
    }
}
