namespace WodItEasy.Domain.Models.Athletes
{
    using System;
    using Common;
    using Exceptions;

    using static ModelConstants.Common;
    using static ModelConstants.Membership;

    public class Membership : Entity<int>
    {
        internal Membership(
            MembershipType membershipType,
            int? workoutsCount,
            DateTime startsAt)
        {
            this.Validate(membershipType, workoutsCount, startsAt);

            this.Type = membershipType;
            this.WorkoutsCount = workoutsCount;
            this.WorkoutsLeft = workoutsCount;
            this.StartsAt = startsAt;
        }

        public MembershipType Type { get; }

        public int? WorkoutsCount { get; }

        public int? WorkoutsLeft { get; }

        public DateTime StartsAt { get; }

        public bool IsActive()
        {
            if (this.Type == MembershipType.Monthly)
            {
                return DateTime.Now <= this.StartsAt.AddMonths(1);
            }

            return this.WorkoutsLeft != Zero;
        }

        private void Validate(
            MembershipType membershipType,
            int? workoutsCount,
            DateTime startsAt)
        {
            Guard.AgainstOutOfRange<InvalidMembershipException>(
               startsAt,
               MinStartDateValue,
               MaxStartDateValue,
               nameof(this.StartsAt));

            if (membershipType == MembershipType.FixedWorkouts &&
                workoutsCount is null)
            {
                throw new InvalidMembershipException("Fixed workouts memberships should have workouts count!");
            }

            if (membershipType == MembershipType.Monthly &&
                workoutsCount is not null)
            {
                throw new InvalidMembershipException("Monthly memberships should not have workouts count!");
            }
        }
    }
}
