namespace WodItEasy.Domain.Factories.Workout
{
    using System;
    using Models.Workouts;

    public class WorkoutFactory : IWorkoutFactory
    {
        private string name = default!;
        private string imageUrl = default!;
        private string description = default!;
        private int maxParticipantsCount;
        private DateTime startsAt;
        private WorkoutType type = default!;

        public IWorkoutFactory WithName(string name)
        {
            this.name = name;

            return this;
        }

        public IWorkoutFactory WithImageUrl(string imageUrl)
        {
            this.imageUrl = imageUrl;

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

        public IWorkoutFactory StartsAt(DateTime startsAt)
        {
            this.startsAt = startsAt;

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
                this.imageUrl,
                this.description,
                this.maxParticipantsCount,
                this.startsAt,
                this.type);
    }
}
