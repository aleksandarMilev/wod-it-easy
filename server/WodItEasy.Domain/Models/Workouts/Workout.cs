namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Athletes;
    using Common;
    using Exceptions;
   
    using static ModelConstants.Workout;

    public class Workout : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Athlete> athletes = [];

        public Workout(
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
            this.WorkoutType = workoutType;
        }

        public string Name { get; }

        public string Description { get; }

        public int MaxParticipantsCount { get; }

        public int CurrentParticipantsCount 
            => this.athletes.Count;

        public DateTime StartsAtDate { get; }

        public TimeSpan StartsAtTime { get; }

        public WorkoutType WorkoutType { get; }

        public IReadOnlyCollection<Athlete> Athletes
            => this.athletes.ToList().AsReadOnly();

        private bool IsClosed()
            => DateTime.Now > this.StartsAtDate;

        private bool IsFull()
            => this.CurrentParticipantsCount == this.MaxParticipantsCount;

        public bool AddAthlete(Athlete athlete)
        {
            if (this.IsClosed())
            {
                throw new RemoveAthleteException($"Workout is closed! Occurred on {this.StartsAtDate:dd MMM yyyy}!");
            }

            if (this.IsFull())
            {
                throw new AddAthleteException($"The workout has reached its maximum participant limit of {this.MaxParticipantsCount}!");
            }

            return this.athletes.Add(athlete);
        }

        public bool RemoveAthlete(Athlete athlete)
        {
            if (this.IsClosed())
            {
                throw new RemoveAthleteException($"Workout is closed! Occurred on {this.StartsAtDate:dd MMM yyyy}!");
            }

            return this.athletes.Remove(athlete);
        }

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
