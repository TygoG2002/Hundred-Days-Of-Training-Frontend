using System.Net.Http.Json;
using HundredDays.Models;

namespace HundredDays.Services;

public class WorkoutApi
{
    private readonly HttpClient _http;

    public WorkoutApi(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<WorkoutPlanDto>> GetPlans()
    {
        return await _http.GetFromJsonAsync<List<WorkoutPlanDto>>(
            "api/plans")
            ?? new();
    }

    public async Task<List<int>> GetDays(int planId)
    {
        return await _http.GetFromJsonAsync<List<int>>(
            $"api/plans/{planId}/days")
            ?? new();
    }

    public async Task<List<WorkoutSetDto>> GetSets(int planId, int day)
    {
        return await _http.GetFromJsonAsync<List<WorkoutSetDto>>(
            $"api/plans/{planId}/days/{day}/sets")
            ?? new();
    }

    public async Task UpdateSet(int planId, int day, int index, bool completed)
    {
        await _http.PostAsJsonAsync(
            $"api/plans/{planId}/days/{day}/sets/{index}",
            completed);
    }

    public async Task<(int done, int total)> GetDayProgress(int planId, int day)
    {
        var sets = await GetSets(planId, day);
        return (
            sets.Count(s => s.Completed),
            sets.Count
        );
    }
}
