namespace WodItEasy.Domain.Factories.Workout
{
    using System;
    using Models.Workouts;
    using WodItEasy.Domain.Common;

    public class WorkoutFactory : IWorkoutFactory
    {
        private string name = default!;
        private string description = default!;
        private int maxParticipantsCount;
        private DateTime startsAtDate;
        private TimeSpan startsAtTime;
        private WorkoutType type = default!;

        public IWorkoutFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IWorkoutFactory WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public IWorkoutFactory WithMaxParticipantsCount(int maxParticipantsCount)
        {
            this.maxParticipantsCount = maxParticipantsCount;
            return this;
        }

        public IWorkoutFactory WithStartsAtDate(DateTime startsAtDate)
        {
            this.startsAtDate = startsAtDate;
            return this;
        }

        public IWorkoutFactory WithStartsAtTime(TimeSpan startsAtTime)
        {
            this.startsAtTime = startsAtTime;
            return this;
        }

        public IWorkoutFactory WithType(WorkoutType type)
        {
            this.type = type;
            return this;
        }

        public Workout Build()
            => new(
                this.name,
                this.description,
                this.maxParticipantsCount,
                this.startsAtDate,
                this.startsAtTime,
                this.type);
    }
}
