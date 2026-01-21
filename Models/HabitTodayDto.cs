namespace HundredDays.Models
{
    public class HabitTodayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TargetValue { get; set; }

        public bool IsValueBased { get; set; }
        public int TodayValue { get; set; }
        public bool TodayCompleted { get; set; }
    }

}
