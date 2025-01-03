namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Athletes;
    using Common;
    using Exceptions;

    using static ModelConstants.WorkoutConstants;

    public class Workout : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Athlete> athletes = [];

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

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int MaxParticipantsCount { get; private set; }

        public int CurrentParticipantsCount { get; private set; }

        public DateTime StartsAtDate { get; private set; }

        public TimeSpan StartsAtTime { get; private set; }

        public WorkoutType Type { get; private set; }

        public IReadOnlyCollection<Athlete> Athletes => this.athletes.ToList().AsReadOnly();

        private bool IsClosed() => DateTime.Now > this.StartsAtDate;

        private bool IsFull() => this.CurrentParticipantsCount == this.MaxParticipantsCount;

        private void IncrementParticipantsCount() => this.CurrentParticipantsCount++;

        private void DecrementParticipantsCount() => this.CurrentParticipantsCount--;

        public void AddParticipant(Athlete athlete)
        {
            if (this.IsClosed())
            {
                throw new WorkoutClosedException("Workout is closed.");
            }

            if (this.IsFull())
            {
                throw new WorkoutFullException("Workout is full.");
            }

            if (!athlete.HasMembership())
            {
                throw new MembershipExpiredException("Athlete has not active membership!");
            }

            this.athletes.Add(athlete);
            this.IncrementParticipantsCount();
        }

        public void RemoveParticipant(Athlete athlete)
        {
            var athleteIsJoined = this.athletes
                .Any(a => a.Id == athlete.Id);

            if (!athleteIsJoined)
            {
                throw new RemoveAthleteException("Athlete is not part of this workout.");
            }

            if (this.IsClosed())
            {
                throw new WorkoutClosedException("Workout is closed.");
            }

            this.athletes.Remove(athlete);
            this.DecrementParticipantsCount();
        }

        public Workout UpdateName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        public Workout UpdateDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        public Workout UpdateMaxParticipantsCount(int maxParticipantsCount)
        {
            this.ValidateMaxParticipantsCount(maxParticipantsCount);
            this.MaxParticipantsCount = maxParticipantsCount;

            return this;
        }

        public Workout UpdateStartsAtDate(DateTime startsAtDate)
        {
            this.ValidateStartsAtDate(startsAtDate);
            this.StartsAtDate = startsAtDate;

            return this;
        }

        public Workout UpdateStartsAtTime(TimeSpan startsAtTime)
        {
            this.StartsAtTime = startsAtTime;

            return this;
        }

        public Workout UpdateType(WorkoutType type)
        {
            this.Type = type;

            return this;
        }

        private void Validate(
            string name,
            string description,
            int maxParticipantsCount,
            DateTime startsAtDate)
        {
            this.ValidateName(name);
            this.ValidateDescription(description);
            this.ValidateMaxParticipantsCount(maxParticipantsCount);
            this.ValidateStartsAtDate(startsAtDate);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidWorkoutException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidWorkoutException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));

        private void ValidateMaxParticipantsCount(int maxParticipantsCount)
            => Guard.AgainstOutOfRange<InvalidWorkoutException>(
                maxParticipantsCount,
                MaxParticipantsCountMinValue,
                MaxParticipantsCountMaxValue,
                nameof(this.MaxParticipantsCount));

        private void ValidateStartsAtDate(DateTime startsAtDate)
            => Guard.AgainstOutOfRange<InvalidWorkoutException>(
                startsAtDate,
                MinStartAtDateValue,
                MaxStartAtDateValue,
                nameof(this.StartsAtDate));
    }
}
