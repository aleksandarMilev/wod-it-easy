namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using Common;
    using Exceptions;
   
    using static ModelConstants.WorkoutConstants;

    public class Workout : Entity<int>, IAggregateRoot
    {
        internal Workout(
             string name,
             string description,
             int maxParticipantsCount,
             DateTime startsAtDate,
             TimeSpan startsAtTime,
             WorkoutType workoutType)
        {
            this.Validate(name, description, maxParticipantsCount, startsAtDate);

            this.Name = name;
            this.Description = description;
            this.MaxParticipantsCount = maxParticipantsCount;
            this.StartsAtDate = startsAtDate;
            this.StartsAtTime = startsAtTime;
            this.Type = workoutType;
        }

        private Workout(string name, string description)
        {
            this.Name = name;
            this.Description = description;

            this.MaxParticipantsCount = default;
            this.StartsAtDate = default;
            this.StartsAtTime = default;
            this.Type = default!;
        }

        public string Name { get; }

        public string Description { get; }

        public int MaxParticipantsCount { get; }

        public int CurrentParticipantsCount { get; private set; }

        public DateTime StartsAtDate { get; }

        public TimeSpan StartsAtTime { get; }

        public WorkoutType Type { get; }

        public bool IsClosed() => DateTime.Now > this.StartsAtDate;

        public bool IsFull() => this.CurrentParticipantsCount == this.MaxParticipantsCount;

        public void IncrementParticipantsCount() => this.CurrentParticipantsCount++;

        public void DecrementParticipantsCount() => this.CurrentParticipantsCount--;

        private void Validate(
            string name,
            string description,
            int maxParticipantsCount,
            DateTime startsAtDate)
        {
            Guard.ForStringLength<InvalidWorkoutException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.ForStringLength<InvalidWorkoutException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));

            Guard.AgainstOutOfRange<InvalidWorkoutException>(
                maxParticipantsCount,
                MaxParticipantsCountMinValue,
                MaxParticipantsCountMaxValue,
                nameof(this.MaxParticipantsCount));

            Guard.AgainstOutOfRange<InvalidWorkoutException>(
                startsAtDate,
                MinStartAtDateValue,
                MaxStartAtDateValue,
                nameof(this.StartsAtDate));
        }
    }
}
