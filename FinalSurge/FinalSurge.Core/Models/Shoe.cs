namespace FinalSurge.Core.Models;

public class Shoe
{
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Size { get; set; } = string.Empty;
    public int AlertDistance { get; set; }
    public string AlertDistanceUnit { get; set; } = "km";
}
