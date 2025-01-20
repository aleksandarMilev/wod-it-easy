namespace WodItEasy.Domain.Events
{
    using System;
    using Common;

    public record AthleteJoinedWorkout(
        int AthleteId, 
        int WorkoutId, 
        DateTime JoinedAt) : IDomainEvent;
}
