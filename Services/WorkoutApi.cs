using System.Net.Http.Json;
using HundredDays.Models;

namespace HundredDays.Services;

public class WorkoutApi
{
    private readonly HttpClient _http;

    //caches - after api gets data 1 time it saves here for future uses to limit api calls 
    private List<WorkoutPlanDto>? _plansCache;
    private List<PlanOverviewDto>? _plansOverviewCache;
    private readonly Dictionary<int, List<int>> _daysCache = new();
    private readonly Dictionary<(int planId, int day), List<WorkoutSetDto>> _setsCache = new();
    private readonly Dictionary<(int planId, int day), (int done, int total)> _dayProgressCache = new();

    public WorkoutApi(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<WorkoutPlanDto>> GetPlans()
    {
        if (_plansCache != null)
            return _plansCache;

        _plansCache =
            await _http.GetFromJsonAsync<List<WorkoutPlanDto>>("api/plans")
            ?? new();

        return _plansCache;
    }

    public async Task<List<PlanOverviewDto>> GetPlansOverview()
    {
        if (_plansOverviewCache != null)
            return _plansOverviewCache;

        _plansOverviewCache =
            await _http.GetFromJsonAsync<List<PlanOverviewDto>>("api/plans/overview")
            ?? new();

        return _plansOverviewCache;
    }

    public async Task<List<int>> GetDays(int planId)
    {
        if (_daysCache.TryGetValue(planId, out var cached))
            return cached;

        var days =
            await _http.GetFromJsonAsync<List<int>>($"api/plans/{planId}/days")
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
            await _http.GetFromJsonAsync<List<WorkoutSetDto>>(
                $"api/plans/{planId}/days/{day}/sets")
            ?? new();

        _setsCache[key] = sets;
        return sets;
    }

    public async Task UpdateSet(int setId, bool completed, int planId, int day)
    {
        await _http.PostAsJsonAsync(
            $"api/plans/sets/{setId}",
            completed);
        _setsCache.Remove((planId, day));
        _dayProgressCache.Remove((planId, day));
        _plansOverviewCache = null;
    }

    public async Task<(int done, int total)> GetDayProgress(int planId, int day)
    {
        var key = (planId, day);

        if (_dayProgressCache.TryGetValue(key, out var cached))
            return cached;

        var result =
            await _http.GetFromJsonAsync<DayProgressDto>(
                $"api/plans/{planId}/days/{day}/progress");

        var progress = (result!.Done, result.Total);
        _dayProgressCache[key] = progress;

        return progress;
    }


    public void InvalidatePlan(int planId)
    {
        _daysCache.Remove(planId);

        foreach (var key in _setsCache.Keys
                     .Where(k => k.planId == planId)
                     .ToList())
        {
            _setsCache.Remove(key);
        }

        foreach (var key in _dayProgressCache.Keys
                     .Where(k => k.planId == planId)
                     .ToList())
        {
            _dayProgressCache.Remove(key);
        }

        _plansOverviewCache = null;
    }
}
