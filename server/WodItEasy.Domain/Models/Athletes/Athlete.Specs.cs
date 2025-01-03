namespace WodItEasy.Domain.Models.Athletes
{
    using Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using Xunit;

    public class AthleteSpecs
    {
        [Fact]
        public void ConstructorShouldCreateAValidEntity()
        {
            var constructor = () => new Athlete("My Name");

            constructor.Should().NotThrow<InvalidAthleteException>();
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void ConstructorShouldThrowExceptionIfNameIsNotValid(string invalidName)
        {
            var constructor = () => new Athlete(invalidName);

            constructor.Should().Throw<InvalidAthleteException>();
        }

        [Fact]
        public void UpdateNameShouldSetNameProperty()
        {
            var athlete = A.Dummy<Athlete>();
            var newName = "newName";

            athlete.UpdateName(newName);

            athlete.Name.Should().Be(newName);
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void UpdateNameShouldThrowExceptionIfNameIsNotValid(string invalidName)
        {
            var athlete = A.Dummy<Athlete>();

            var updateName = () => athlete.UpdateName(invalidName);

            updateName.Should().Throw<InvalidAthleteException>();
        }

        [Fact]
        public void HasMembershipShouldReturnTrueIfMembershipIsNotNull()
        {
            var athlete = A.Dummy<Athlete>();

            athlete.HasMembership().Should().BeTrue();
        }

        [Fact]
        public void HasMembershipShouldReturnFalseIfMembershipIsNull()
        {
            var athlete = A.Dummy<Athlete>();
            athlete.DeleteMembership();

            athlete.HasMembership().Should().BeFalse();
        }

        [Fact]
        public void CreateMembershipShouldSetMembershipProperty()
        {
            var athlete = A.Dummy<Athlete>();
            athlete.DeleteMembership();

            var startDate = DateTime.Now;
            var membership = new Membership(MembershipType.FixedWorkouts, startDate, 21);

            athlete.CreateMembership(membership);

            athlete.Membership.Should().NotBeNull();
            athlete.Membership!.Type.Should().Be(MembershipType.FixedWorkouts);
            athlete.Membership!.StartsAt.Should().Be(startDate);
            athlete.Membership!.WorkoutsCount.Should().Be(21);
            athlete.Membership!.WorkoutsLeft.Should().Be(21);
        }

        [Fact]
        public void UpdateMembershipShouldSetMembershipProperty()
        {
            var athlete = A.Dummy<Athlete>();
            athlete.DeleteMembership();

            var startDate = DateTime.Now.AddDays(2);
            var membership = new Membership(MembershipType.Monthly, startDate);

            athlete.UpdateMembership(membership);

            athlete.Membership.Should().NotBeNull();
            athlete.Membership!.Type.Should().Be(MembershipType.Monthly);
            athlete.Membership!.StartsAt.Should().Be(startDate);
            athlete.Membership!.WorkoutsCount.Should().BeNull();
            athlete.Membership!.WorkoutsLeft.Should().BeNull();
        }

        [Fact]
        public void DeleteMembershipShouldSetMembershipPropertyToNull()
        {
            var athlete = A.Dummy<Athlete>();

            athlete.DeleteMembership();

            athlete.Membership.Should().BeNull();
        }

        public static TheoryData<string> InvalidNames => 
            [
                null!,
                string.Empty,
                "a",
                new('a', 101)
            ];
    }
}
