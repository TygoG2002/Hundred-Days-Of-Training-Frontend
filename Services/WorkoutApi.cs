using System.Net.Http.Json;
using HundredDays.Models;

namespace HundredDays.Services;

public class WorkoutApi
{
    private readonly HttpClient _http;

    private List<WorkoutPlanDto>? _plansCache;
    private List<PlanOverviewDto>? _plansOverviewCache;
    private readonly Dictionary<int, List<DayOverviewDto>> _daysCache = new();
    private List<WorkoutTemplatesDto> _templatesCache; 

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

    /* WORKOUT TEMPLATES */
    public async Task<List<WorkoutTemplatesDto>> GetTemplates()
    {
        if (_templatesCache != null)
            return _templatesCache;

        _templatesCache = await SafeGet<List<WorkoutTemplatesDto>>("api/Templates");
        return _templatesCache;
    }

    /* WORKOUT SESSIONS */
    public async Task<WorkoutSessionDto> StartWorkoutSession(int workoutTemplateId)
    {
        var response = await _http.PostAsJsonAsync(
            "api/sessions/start",
            workoutTemplateId); 

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<WorkoutSessionDto>();

        if (result == null)
            throw new Exception("Empty response when starting workout session");

        return result;
    }


    public async Task<WorkoutSessionDetailsDto> GetWorkoutSession(int sessionId)
    {
        return await SafeGet<WorkoutSessionDetailsDto>(
            $"api/sessions/{sessionId}");
    }

    public async Task FinishWorkoutSession(
      int sessionId,
      List<FinishWorkoutSessionExerciseDto> exercises)
    {
        var response = await _http.PostAsJsonAsync(
            $"api/sessions/{sessionId}/finish",
            new
            {
                sessionId,
                exercises
            });

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteWorkoutSession(int sessionId)
    {
        var response = await _http.DeleteAsync(
            $"api/sessions/delete/{sessionId}");

        response.EnsureSuccessStatusCode();
    }

    /* HABITS */

    public async Task<List<HabitTodayDto>> GetHabitsAsync()
    {
        return await _http.GetFromJsonAsync<List<HabitTodayDto>>("api/habits")
               ?? new();
    }

    public async Task UpdateHabitValue(int habitId, int amount)
    {
        await _http.PostAsJsonAsync(
            $"api/habits/{habitId}/add-value",
            new { amount });
    }



    /* DASHBOARD */

    public async Task<TodayDashboardDto> GetTodayWorkouts()
    {
        return await SafeGet<TodayDashboardDto>(
            "api/dashboard/today");
    }

    public async Task<List<WeekPlanningDto>> GetDayOfWeekPlanning()
    {
        return await SafeGet<List<WeekPlanningDto>>(
            "api/dashboard/week");
    }

    public async Task UpdateWeekPlanning(int id, int dayOfWeek)
    {
        var response = await _http.PutAsJsonAsync(
            "api/dashboard/updatePlanning",
            new
            {
                id,
                dayOfWeek
            });

        response.EnsureSuccessStatusCode();
    }



    /* CACHE INVALIDATION */

    public void InvalidatePlan(int planId)
    {
        _daysCache.Remove(planId);
        _plansOverviewCache = null;
    }
}
