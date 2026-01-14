namespace HundredDays.Models
{
    public class TodayDashboardDto
    {
        public List<PlanOverviewDto> Plans { get; set; } = new();
        public List<TodayTemplateDto> Templates { get; set; } = new();
    }
}
