namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using System;

    public class WorkoutDetailsOutputModel
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public int MaxParticipantsCount { get; init; }

        public int CurrentParticipantsCount { get; init; }

        public DateTime StartsAtDate { get; init; }

        public TimeSpan StartsAtTime { get; init; }

        public string Type { get; init; } = null!;
    }
}
