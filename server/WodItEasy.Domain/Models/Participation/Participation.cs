namespace WodItEasy.Domain.Models.Participation
{
    using System;
    using Athletes;
    using Common;
    using Exceptions;
    using Workouts;

    public class Participation : AuditableEntity<int>, IAggregateRoot
    {
        public Athlete? Athlete { get; }

        public int AthleteId { get; }

        public Workout? Workout { get; }

        public int WorkoutId { get; }

        public DateTime JoinedAt { get; }

        public ParticipationStatus Status { get; private set; }

        internal Participation(
            Athlete athlete, 
            Workout workout,
            DateTime joinedAt,
            ParticipationStatus status)
        {
            Validate(workout);

            this.Athlete = athlete;
            this.Workout = workout;
            this.JoinedAt = joinedAt;
            this.Status = status;
        }

        private Participation(DateTime joinedAt)
        {
            this.Athlete = default!;
            this.Workout = default!;
            this.Status = default!;

            this.JoinedAt = joinedAt;
        }

        public void MarkAsLeft() 
            => this.Status = ParticipationStatus.Left;

        public void MarkAsJoined()
            => this.Status = ParticipationStatus.Joined;

        private static void Validate(Workout workout)
        {
            if (workout.IsClosed())
            {
                throw new WorkoutClosedException("The workout is closed for joining!");
            }

            if (workout.IsFull())
            {
                throw new WorkoutFullException("The workout reached max participants count!");
            }

            workout.IncrementParticipantsCount();
        }
    }
}
