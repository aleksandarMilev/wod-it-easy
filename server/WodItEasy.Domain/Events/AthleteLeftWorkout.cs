namespace WodItEasy.Domain.Events
{
    using System;
    using Common;

    public record AthleteLeftWorkout(int AthleteId, int WorkoutId)
        : IDomainEvent
    {
        public DateTime LeftAt { get; } = DateTime.UtcNow;
    }
}