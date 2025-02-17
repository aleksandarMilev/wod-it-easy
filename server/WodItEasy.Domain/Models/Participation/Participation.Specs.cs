namespace WodItEasy.Domain.Models.Participation
{
    using System;
    using Athletes;
    using Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using Workouts;
    using Xunit;

    public class ParticipationSpecs
    {
        [Fact]
        public void ConstructorShouldCreateValidParticipation()
        {
            var participation = A.Dummy<Participation>();

            participation.Should().NotBeNull();
            participation.Status.Should().Be(ParticipationStatus.Joined);
        }

        [Fact]
        public void ConstructorShouldThrowExceptionIfWorkoutIsFull()
        {
            var athlete = new Athlete("Some Athlete", "userId");
            var workout = new Workout(
                "Test",
                "https://test-url.com/some-image",
                "Description",
                1,
                DateTime.UtcNow.AddDays(1).Add(TimeSpan.FromHours(12)),
                WorkoutType.CrossFit);

            workout.IncrementParticipantsCount();

            var constructor = () => new Participation(
                athlete,
                workout,
                DateTime.Now,
                ParticipationStatus.Joined);

            constructor
                .Should()
                .Throw<WorkoutFullException>();
        }

        [Fact]
        public void MarkAsLeftShouldChangeStatusToLeft()
        {
            var participation = A.Dummy<Participation>();

            participation.MarkAsLeft();

            participation.Status.Should().Be(ParticipationStatus.Left);
        }

        [Fact]
        public void MarkAsJoinedShouldChangeStatusToJoined()
        {
            var participation = A.Dummy<Participation>();

            participation.MarkAsLeft();
            participation.MarkAsJoined();

            participation.Status.Should().Be(ParticipationStatus.Joined);
        }
    }
}
