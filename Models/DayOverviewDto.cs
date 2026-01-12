namespace HundredDays.Models
{
    public class DayOverviewDto
    {
        public int DayId { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime? CompletedAt { get; set; }
    }

}
