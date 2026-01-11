namespace HundredDays.Models;

public class TodayWorkoutDto
{
    public int PlanId { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public int DayNumber { get; set; }
    public bool IsCompleted { get; set; }
}
