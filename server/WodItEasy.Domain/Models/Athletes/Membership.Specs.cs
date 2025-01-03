namespace WodItEasy.Domain.Models.Athletes
{
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class MembershipSpecs
    {
        [Fact]
        public void ConstructorShouldCreateValidMembership()
        {
            var membership = new Membership(MembershipType.FixedWorkouts, DateTime.Now, 21);

            membership.Should().NotBeNull();
            membership.Type.Should().Be(MembershipType.FixedWorkouts);
            membership.WorkoutsCount.Should().Be(21);
            membership.WorkoutsLeft.Should().Be(21);
            membership.StartsAt.Should().BeOnOrAfter(DateTime.Now.AddMinutes(-1));
        }

        [Fact]
        public void ConstructorShouldThrowExceptionIfTypeIsFixedWorkoutsAndWorkoutsCountIsNull()
        {
            var constructor = () => new Membership(MembershipType.FixedWorkouts, DateTime.Now, null);

            constructor.Should().Throw<InvalidMembershipException>();
        }

        [Fact]
        public void ConstructorShouldThrowExceptionIfTypeIsMonthlyAndWorkoutsCountIsNotNull()
        {
            var constructor = () => new Membership(MembershipType.Monthly, DateTime.Now, 21);

            constructor.Should().Throw<InvalidMembershipException>();
        }

        [Fact]
        public void ConstructorShouldThrowExceptionIfTypeIsFixedWorkoutsAndWorkoutsCountIsLessThanOne()
        {
            var constructor = () => new Membership(MembershipType.FixedWorkouts, DateTime.Now, 0);

            constructor.Should().Throw<InvalidMembershipException>();
        }

        [Fact]
        public void ConstructorShouldThrowExceptionIfTypeIsFixedWorkoutsAndWorkoutsCountIsMoreThanThirtyOne()
        {
            var constructor = () => new Membership(MembershipType.FixedWorkouts, DateTime.Now, 32);

            constructor.Should().Throw<InvalidMembershipException>();
        }

        [Fact]
        public void IsActiveShouldReturnTrueForActiveMembership()
        {
            var startDate = DateTime.Now;
            var membership = new Membership(MembershipType.FixedWorkouts, startDate, 21);

            membership.IsActive().Should().BeTrue();
        }
    }
}
