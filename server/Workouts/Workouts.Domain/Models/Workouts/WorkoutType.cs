﻿namespace WodItEasy.Workouts.Domain.Models.Workouts
{
    using Common.Domain.Models;

    public class WorkoutType : Enumeration
    {
        public static readonly WorkoutType Weightlifting = new(1, nameof(Weightlifting));
        public static readonly WorkoutType Gymnastic = new(2, nameof(Gymnastic));
        public static readonly WorkoutType Cardiovascular = new(3, nameof(Cardiovascular));
        public static readonly WorkoutType Mobility = new(4, nameof(Mobility));
        public static readonly WorkoutType Powerlifting = new(5, nameof(Powerlifting));
        public static readonly WorkoutType CrossFit = new(6, nameof(CrossFit));
        public static readonly WorkoutType Other = new(7, nameof(Other));

        private WorkoutType(int value, string name)
           : base(value, name) { }

        // EF Core needs it
        private WorkoutType(int value) 
            : this(value, FromValue<WorkoutType>(value).Name) { }
    }
}
