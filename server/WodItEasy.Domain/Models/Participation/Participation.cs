namespace WodItEasy.Domain.Models.Participation
{
    using System;
    using Athletes;
    using Common;
    using Workouts;

    public class Participation : Entity<int>, IAggregateRoot
    {
        public int AthleteId { get; }

        public Athlete? Athlete { get; }

        public int WorkoutId { get; }

        public Workout? Workout { get; }

        public DateTime JoinedAt { get; }

        public ParticipationStatus Status { get; private set; }

        internal Participation(int athleteId, int workoutId)
        {
            this.AthleteId = athleteId;
            this.WorkoutId = workoutId;
            this.JoinedAt = DateTime.UtcNow;
            this.Status = ParticipationStatus.Joined;
        }

        public void MarkAsLeft() 
            => this.Status = ParticipationStatus.Left;
    }
}
