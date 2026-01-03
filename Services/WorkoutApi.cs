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


    //get all days 
    public async Task<List<int>> GetDays(int planId)
    {
        return await _http.GetFromJsonAsync<List<int>>(
            $"api/plans/{planId}/days")
            ?? new();
    }

    //get sets based on the plan and the selected day 
    public async Task<List<WorkoutSetDto>> GetSets(int planId, int day)
    {
        return await _http.GetFromJsonAsync<List<WorkoutSetDto>>(
            $"api/plans/{planId}/days/{day}/sets")
            ?? new();
    }
    public async Task UpdateSet(int setId, bool completed)
    {
        await _http.PostAsJsonAsync(
            $"api/plans/sets/{setId}",
            completed);
    }
    public async Task<List<PlanOverviewDto>> GetPlansOverview()
    {
        return await _http.GetFromJsonAsync<List<PlanOverviewDto>>(
            "api/plans/overview")
            ?? new();
    }



    public async Task<(int done, int total)> GetDayProgress(int planId, int day)
    {
        var result = await _http.GetFromJsonAsync<DayProgressDto>(
            $"api/plans/{planId}/days/{day}/progress");

        return (result!.Done, result.Total);
    }

}
