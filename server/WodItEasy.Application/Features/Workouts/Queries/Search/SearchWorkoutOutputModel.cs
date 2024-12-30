namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System;

    public class SearchWorkoutOutputModel
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public int MaxParticipantsCount { get; init; }

        public int CurrentParticipantsCount { get; init; }

        public DateTime StartsAtDate { get; init; }

        public TimeSpan StartsAtTime { get; init; }

        public string Type { get; init; } = null!;
    }
}
