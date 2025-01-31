﻿namespace WodItEasy.Domain.Events
{
    using System;
    using Common;

    public record AthleteLeftWorkout(
        int AthleteId, 
        int WorkoutId, 
        DateTime LeftAt) : IDomainEvent;
}
