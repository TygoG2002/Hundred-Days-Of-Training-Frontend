namespace HundredDays.Models;

public class FinishWorkoutSessionExerciseDto
{
    public int WorkoutSessionExerciseId { get; set; }

    public List<FinishWorkoutSessionSetDto> Sets { get; set; } = [];
}
