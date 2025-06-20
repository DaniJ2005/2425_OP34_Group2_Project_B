using System;
using System.Globalization;

public class MovieSession
{
    public int Id { get; set; }
    public int MovieHallId { get; set; }
    public int MovieId { get; set; }
    public string StartTime { get; set; }   // "HH:mm"
    public string Date { get; set; }        // "yyyy-MM-dd"
    public string MovieDuration { get; set; }  // "HH:mm" from DB

    // Computed Start
    public DateTime StartDateTime => DateTime.ParseExact($"{Date} {StartTime}", "yyyy-MM-dd HH:mm", null);

    // Parse the duration to a TimeSpan for math
    public TimeSpan Duration => TimeSpan.ParseExact(MovieDuration, @"hh\:mm", null);

    public DateTime EndDateTime => StartDateTime + Duration;

    // INNER JOIN ticket -> Movie
    public string MovieTitle { get; set; }
    // INNER JOIN ticket -> MovieHall
    public string MovieHallName { get; set; }
}