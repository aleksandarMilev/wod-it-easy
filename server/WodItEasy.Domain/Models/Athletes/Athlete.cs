namespace WodItEasy.Domain.Models.Athletes
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Exceptions;
    using Workouts;

    using static ModelConstants.AthleteConstants;

    public class Athlete : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Workout> workouts = [];

        public Athlete(
            string name,
            string email,
            string phoneNumber,
            Membership? membership = null)
        {
            this.Validate(name, email);

            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Membership = membership;
        }

        private Athlete(
            string name,
            string email)
        {
            this.Name = name;
            this.Email = email;

            this.PhoneNumber = default!;
            this.Membership = default;
        }

        public string Name { get; }

        public string Email { get; }

        public PhoneNumber PhoneNumber { get; }

        public Membership? Membership { get; private set; }

        public IReadOnlyCollection<Workout> Workouts => this.workouts.ToList().AsReadOnly();

        public bool JoinWorkout(Workout workout)
        {
            if (!this.HasMembership())
            {
                throw new MembershipExpiredException("This athlete has not active membership!");
            }

            if (workout.IsClosed())
            {
                throw new AddAthleteException($"WorkoutConstants is closed! Occurred on {workout.StartsAtDate:dd MMM yyyy}!");
            }

            if (workout.IsFull())
            {
                throw new AddAthleteException($"The workout has reached its maximum participant limit of {workout.MaxParticipantsCount}!");
            }

            workout.IncrementParticipantsCount();

            return this.workouts.Add(workout);
        }

        public bool LeaveWorkout(Workout workout)
        {
            if (workout.IsClosed())
            {
                throw new RemoveAthleteException($"WorkoutConstants is closed! Occurred on {workout.StartsAtDate:dd MMM yyyy}!");
            }

            workout.DecrementParticipantsCount();

            return this.workouts.Remove(workout);
        }

        public bool HasMembership() => this.Membership! != null!;

        public void CreateMembership(Membership membership) => this.Membership = membership;

        public void DeleteMembership() => this.Membership = null;

        private void Validate(string name, string email)
        {
            Guard.ForStringLength<InvalidAthleteException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.ForValidEmail<InvalidAthleteException>(
                email,
                nameof(this.Email));
        }
    }
}
