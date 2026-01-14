namespace HundredDays.Models;

public class WorkoutSessionSetDto
{
    public int SetNumber { get; set; }

    public int? LastReps { get; set; }
    public decimal? LastWeight { get; set; }

    public int? CurrentReps { get; set; }
    public decimal? CurrentWeight { get; set; }

    public bool Completed { get; set; }

}
