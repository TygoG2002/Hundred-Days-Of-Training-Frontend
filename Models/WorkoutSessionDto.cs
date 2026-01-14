namespace HundredDays.Models
{
    public class WorkoutSessionDto
    {
        public int Id { get; set; }
        public int WorkoutTemplateId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }

}
