namespace HundredDays.Models
{

    public class WorkoutSessionDetailsDto
    {
        public int SessionId { get; set; }
        public int WorkoutTemplateId { get; set; }
        public string TemplateName { get; set; } = "";
        public DateTime StartedAt { get; set; }

        public List<WorkoutSessionExerciseDto> Exercises { get; set; } = [];
    }

}
