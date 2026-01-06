using System.Net.Http.Json;
using HundredDays.Models;

namespace HundredDays.Services;

public class WorkoutApi
{
    private readonly HttpClient _http;

    private List<WorkoutPlanDto>? _plansCache;
    private List<PlanOverviewDto>? _plansOverviewCache;

    private readonly Dictionary<int, List<int>> _daysCache = new();
    private readonly Dictionary<(int planId, int day), List<WorkoutSetDto>> _setsCache = new();
    private readonly Dictionary<(int planId, int day), (int done, int total)> _dayProgressCache = new();

    public WorkoutApi(HttpClient http)
    {
        _http = http;
    }

    private async Task<T?> SafeGet<T>(string url)
    {
        try
        {
            using var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return default;

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            return default;
        }
    }

    public async Task<List<WorkoutPlanDto>> GetPlans()
    {
        if (_plansCache != null)
            return _plansCache;

        _plansCache =
            await SafeGet<List<WorkoutPlanDto>>("api/plans")
            ?? new();

        return _plansCache;
    }

    public async Task<List<PlanOverviewDto>> GetPlansOverview()
    {
        if (_plansOverviewCache != null)
            return _plansOverviewCache;

        _plansOverviewCache =
            await SafeGet<List<PlanOverviewDto>>("api/plans/overview")
            ?? new();

        return _plansOverviewCache;
    }

    public async Task<List<int>> GetDays(int planId)
    {
        if (_daysCache.TryGetValue(planId, out var cached))
            return cached;

        var days =
            await SafeGet<List<int>>($"api/plans/{planId}/days")
            ?? new();

        _daysCache[planId] = days;
        return days;
    }

    public async Task<List<WorkoutSetDto>> GetSets(int planId, int day)
    {
        var key = (planId, day);

        if (_setsCache.TryGetValue(key, out var cached))
            return cached;

        var sets =
            await SafeGet<List<WorkoutSetDto>>(
                $"api/plans/{planId}/days/{day}/sets")
            ?? new();

        _setsCache[key] = sets;
        return sets;
    }

    public async Task<(int done, int total)> GetDayProgress(int planId, int day)
    {
        var key = (planId, day);

        if (_dayProgressCache.TryGetValue(key, out var cached))
            return cached;

        var result =
            await SafeGet<DayProgressDto>(
                $"api/plans/{planId}/days/{day}/progress");

        if (result == null)
            return (0, 0);

        var progress = (result.Done, result.Total);
        _dayProgressCache[key] = progress;

        return progress;
    }

    public async Task UpdateSet(int setId, bool completed, int planId, int day)
    {
        try
        {
            await _http.PostAsJsonAsync(
                $"api/plans/sets/{setId}",
                completed);
        }
        catch
        {
        }

        _setsCache.Remove((planId, day));
        _dayProgressCache.Remove((planId, day));
        _plansOverviewCache = null;
    }

    public void InvalidatePlan(int planId)
    {
        _daysCache.Remove(planId);

        foreach (var key in _setsCache.Keys.Where(k => k.planId == planId).ToList())
            _setsCache.Remove(key);

        foreach (var key in _dayProgressCache.Keys.Where(k => k.planId == planId).ToList())
            _dayProgressCache.Remove(key);

        _plansOverviewCache = null;
    }
}
