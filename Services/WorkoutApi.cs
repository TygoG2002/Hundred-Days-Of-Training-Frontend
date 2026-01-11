using System.Net.Http.Json;
using HundredDays.Models;

namespace HundredDays.Services;

public class WorkoutApi
{
    private readonly HttpClient _http;

    private List<WorkoutPlanDto>? _plansCache;
    private List<PlanOverviewDto>? _plansOverviewCache;
    private readonly Dictionary<int, List<DayOverviewDto>> _daysCache = new();

    public WorkoutApi(HttpClient http)
    {
        _http = http;
    }

    private async Task<T> SafeGet<T>(string url)
    {
        using var response = await _http.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<T>();

        if (result == null)
            throw new Exception($"Empty response from {url}");

        return result;
    }

    /* PLANS */

    public async Task<List<WorkoutPlanDto>> GetPlans()
    {
        if (_plansCache != null)
            return _plansCache;

        _plansCache = await SafeGet<List<WorkoutPlanDto>>("api/plans");
        return _plansCache;
    }

    public async Task<List<PlanOverviewDto>> GetPlansOverview()
    {
        if (_plansOverviewCache != null)
            return _plansOverviewCache;

        _plansOverviewCache =
            await SafeGet<List<PlanOverviewDto>>("api/plans/overview");

        return _plansOverviewCache;
    }

    /* DAYS */

    public async Task<List<DayOverviewDto>> GetDays(int planId)
    {
        if (_daysCache.TryGetValue(planId, out var cached))
            return cached;

        var days =
            await SafeGet<List<DayOverviewDto>>(
                $"api/days/{planId}/days");

        _daysCache[planId] = days;
        return days;
    }

    /* SETS */

    public async Task<List<WorkoutSetDto>> GetSets(int planId, int day)
    {
        return await SafeGet<List<WorkoutSetDto>>(
            $"api/sets/{planId}/days/{day}/sets");
    }

    /* COMPLETE DAY */

    public async Task CompleteDay(int planId, int dayId, bool completed)
    {
        var response = await _http.PostAsJsonAsync(
            "api/days/completed",
            new { planId, dayId, completed });

        response.EnsureSuccessStatusCode();
    }

    /* DASHBOARD */

    public async Task<List<PlanOverviewDto>> GetTodayWorkouts()
    {
        return await SafeGet<List<PlanOverviewDto>>(
            "api/dashboard/today");
    }
    

    /* CACHE INVALIDATION */

    public void InvalidatePlan(int planId)
    {
        _daysCache.Remove(planId);
        _plansOverviewCache = null;
    }
}
