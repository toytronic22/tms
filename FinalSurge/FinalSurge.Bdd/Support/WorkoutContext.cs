using FinalSurge.Core.Models;

namespace FinalSurge.Bdd.Support;

public class WorkoutContext
{
    public Workout Workout { get; set; } = null!;

    public string WorkoutKey { get; set; } = string.Empty;

    public List<string> CreatedWorkoutKeys { get; } = new();

    public void Remember(Workout workout, string key)
    {
        Workout = workout;
        WorkoutKey = key;
        CreatedWorkoutKeys.Add(key);
    }
}
