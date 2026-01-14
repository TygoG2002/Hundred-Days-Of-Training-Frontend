namespace HundredDays.Models;

public class WorkoutSessionExerciseDto
{
    public int TemplateExerciseId { get; set; }
    public string Name { get; set; } = "";

    public int Sets { get; set; }
    public int TargetReps { get; set; }

    public List<WorkoutSessionSetDto> SetsData { get; set; } = [];
}

