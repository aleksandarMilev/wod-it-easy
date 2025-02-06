namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using System;
    using Common;

    public record WorkoutDetailsOutputModel(
        int Id,
        string Name,
        string ImageUrl,
        int MaxParticipantsCount,
        int CurrentParticipantsCount,
        DateTime StartsAtDate,
        TimeSpan StartsAtTime,
        string Type,
        string Description) : WorkoutOutputModel(
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
