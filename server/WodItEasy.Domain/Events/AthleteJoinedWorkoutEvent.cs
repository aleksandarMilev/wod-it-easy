namespace WodItEasy.Domain.Events
{
    using System;
    using Common;

    public record AthleteJoinedWorkoutEvent(int AthleteId, int WorkoutId)
        : IDomainEvent
    {
        public DateTime JoinedAt { get; } = DateTime.UtcNow;
    }
}
