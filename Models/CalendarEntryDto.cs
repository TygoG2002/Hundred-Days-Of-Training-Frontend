namespace HundredDays.Models
{
    public class CalendarEntryDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = "";
        public int DayId { get; set; }

        public string ShortName =>
            PlanName.Split(' ')[0];

        public string Color
        {
            get
            {
                var name = PlanName.ToLower();

                if (name.Contains("push")) return "#ffb300";
                if (name.Contains("pull")) return "#29b6f6";
                if (name.Contains("squat")) return "#66bb6a";
                if (name.Contains("core")) return "#ab47bc";

                return "#888";
            }
        }
    }
}
