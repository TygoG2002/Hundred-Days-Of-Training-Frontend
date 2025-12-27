using HundredDays.Models;

namespace HundredDays.Services;

public class WorkoutApi
{
    private readonly Dictionary<(int planId, int day), List<WorkoutSetDto>> _daySets
        = new();

    public async Task<List<WorkoutPlanDto>> GetPlans()
    {
        await Task.Delay(200);

        return new()
        {
            new() { Id = 1, Name = "Push-ups", TargetReps = 100 },
            new() { Id = 2, Name = "Squats", TargetReps = 200 },
            new() { Id = 3, Name = "Pull-ups", TargetReps = 20 }
        };
    }

    public async Task<List<int>> GetDays(int planId)
    {
        await Task.Delay(100);
        return Enumerable.Range(1, 100).ToList();
    }

    public async Task<List<WorkoutSetDto>> GetSets(int planId, int day)
    {
        await Task.Delay(100);

        var key = (planId, day);

        if (!_daySets.ContainsKey(key))
        {
            _daySets[key] = new()
            {
                new() { Reps = 10 },
                new() { Reps = 10 },
                new() { Reps = 15 },
                new() { Reps = 20 }
            };
        }

        return _daySets[key];
    }

    public Task UpdateSet(int planId, int day, int index, bool completed)
    {
        var key = (planId, day);
        _daySets[key][index].Completed = completed;
        return Task.CompletedTask;
    }

    public Task<(int done, int total)> GetDayProgress(int planId, int day)
    {
        var key = (planId, day);

        if (!_daySets.ContainsKey(key))
            return Task.FromResult((0, 0));

        var sets = _daySets[key];
        return Task.FromResult((
            sets.Count(s => s.Completed),
            sets.Count
        ));
    }
}
