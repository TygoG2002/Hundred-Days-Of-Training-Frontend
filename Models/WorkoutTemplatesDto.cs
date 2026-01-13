namespace HundredDays.Models
{
    public class WorkoutTemplatesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }

        public List<TemplateExerciseDto> Exercises { get; set; } = new();
    }
}
