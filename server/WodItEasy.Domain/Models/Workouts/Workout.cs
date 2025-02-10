namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Exceptions;
    using Participation;

    using static ModelConstants.WorkoutConstants;

    public class Workout : DeletableEntity<int>, IAggregateRoot
    {
        private readonly ICollection<Participation> participations = new HashSet<Participation>();

        internal Workout(
             string name,
             string imageUrl,
             string description,
             int maxParticipantsCount,
             DateTime startsAtDate,
             TimeSpan startsAtTime,
             WorkoutType workoutType)
        {
            this.Validate(
                name,
                imageUrl,
                description,
                maxParticipantsCount,
                startsAtDate);

            this.Name = name;
            this.ImageUrl = imageUrl;
            this.Description = description;
            this.MaxParticipantsCount = maxParticipantsCount;
            this.StartsAtDate = startsAtDate;
            this.StartsAtTime = startsAtTime;
            this.Type = workoutType;
        }

        private Workout(
            string name,
            string imageUrl,
            string description)
        {
            this.Name = name;
            this.ImageUrl = imageUrl;
            this.Description = description;

            this.MaxParticipantsCount = default;
            this.StartsAtDate = default;
            this.StartsAtTime = default;
            this.Type = default!;
        }

        public string Name { get; private set; }

        public string ImageUrl { get; private set; }

        public string Description { get; private set; }

        public int MaxParticipantsCount { get; private set; }

        public int CurrentParticipantsCount { get; private set; }

        public DateTime StartsAtDate { get; private set; }

        public TimeSpan StartsAtTime { get; private set; }

        public WorkoutType? Type { get; private set; }

        public IReadOnlyCollection<Participation> Participations
            => this.participations.ToList().AsReadOnly();

        public bool IsClosed()
             => DateTime.Now >= this.StartsAtDate.Add(this.StartsAtTime).AddHours(-2);

        public bool IsFull()
            => this.CurrentParticipantsCount >= this.MaxParticipantsCount;

        public void IncrementParticipantsCount()
            => this.CurrentParticipantsCount++;

        public void DecrementParticipantsCount()
            => this.CurrentParticipantsCount--;

        public bool IsOverlappingExistingOne(IEnumerable<Workout> others)
        {
            foreach (var that in others)
            {
                if (this.OverlapsWith(that))
                {
                    return true;
                }
            }

            return false;
        }

        public Workout UpdateName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        public Workout UpdateImageUrl(string imageUrl)
        {
            this.ValidateImageUrl(imageUrl);
            this.ImageUrl = imageUrl;

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

        private bool OverlapsWith(Workout that)
        {
            if (this.StartsAtDate.Date != that.StartsAtDate.Date)
            {
                return false;
            }

            var thisStartTime = this.StartsAtTime;
            var thisEndTime = this.StartsAtTime.Add(TimeSpan.FromMinutes(59));

            var thatStartTime = that.StartsAtTime;
            var thatEndTime = that.StartsAtTime.Add(TimeSpan.FromMinutes(59));

            return thisStartTime <= thatEndTime &&
                   thisEndTime >= thatStartTime;
        }

        private void Validate(
            string name,
            string imageUrl,
            string description,
            int maxParticipantsCount,
            DateTime startsAtDate)
        {
            this.ValidateName(name);
            this.ValidateImageUrl(imageUrl);
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

        private void ValidateImageUrl(string imageUrl)
            => Guard.ForValidUrl<InvalidWorkoutException>(
                imageUrl,
                nameof(this.ImageUrl));

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidWorkoutException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));

        private void ValidateMaxParticipantsCount(int maxParticipantsCount)
        {
            if (this.CurrentParticipantsCount > maxParticipantsCount)
            {
                throw new InvalidWorkoutException("MaxParticipantsCount value should be greater than or equal to the CurrentParticipantsCount value!");
            }

            Guard.AgainstOutOfRange<InvalidWorkoutException>(
                maxParticipantsCount,
                MaxParticipantsCountMinValue,
                MaxParticipantsCountMaxValue,
                nameof(this.MaxParticipantsCount));
        }

        private void ValidateStartsAtDate(DateTime startsAtDate)
            => Guard.AgainstOutOfRange<InvalidWorkoutException>(
                startsAtDate.Date,
                MinStartAtDateValue,
                MaxStartAtDateValue,
                nameof(this.StartsAtDate));
    }
}
