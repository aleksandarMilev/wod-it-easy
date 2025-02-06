namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System;
    using Common;

    public record SearchWorkoutOutputModel(
        int Id,
        string Name,
        string ImageUrl,
        int MaxParticipantsCount,
        int CurrentParticipantsCount,
        DateTime StartsAtDate,
        TimeSpan StartsAtTime,
        string Type) : WorkoutOutputModel(
            Id,
            Name,
            ImageUrl,
            MaxParticipantsCount,
            CurrentParticipantsCount,
            StartsAtDate,
            StartsAtTime,
            Type)
    {
    }
}
