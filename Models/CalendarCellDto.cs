namespace HundredDays.Models
{
  public class CalendarCellDto
    {
        public DateOnly Date { get; set; }
        public bool IsOutside { get; set; }
        public bool IsToday { get; set; }
        public bool IsFullyCompleted { get; set; }
        public List<CalendarEntryDto> Entries { get; set; } = new();
    }
}
