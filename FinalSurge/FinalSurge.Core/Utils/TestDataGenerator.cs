using FinalSurge.Core.Models;

namespace FinalSurge.Core.Utils;

public static class TestDataGenerator
{
    public const string Prefix = "TMS";

    public static string UniqueName(string name) => $"{Prefix} {name} {Guid.NewGuid().ToString("N")[..8]}";

    public static Workout RunWorkout() => new()
    {
        ActivityType = "Run",
        Date = DateTime.Today,
        Name = UniqueName("Run"),
        Description = "Easy run in the park",
        Distance = 5,
        DistanceUnit = "km",
        Duration = TimeSpan.FromMinutes(30)
    };
}
