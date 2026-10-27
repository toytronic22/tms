namespace FinalSurge.Core.Models;

public class Workout
{
    public string ActivityType { get; set; } = "Run";
    public DateTime Date { get; set; } = DateTime.Today;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Distance { get; set; }
    public string DistanceUnit { get; set; } = "km";
    public TimeSpan Duration { get; set; }
}
