namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using FluentAssertions;
    using WodItEasy.Domain.Exceptions;
    using Xunit;
    using Athletes;
    using FakeItEasy;
    using FluentAssertions.Common;

    public class WorkoutSpecs
    {
        [Fact]
        public void ConstructorShouldCreateValidWorkout()
        {
            var startDate = DateTime.Now.AddDays(1);  
            var startTime = TimeSpan.FromHours(9);  

            var workout = new Workout(
                "Test",
                "A Test Test Test", 
                10, 
                startDate, 
                startTime, 
                WorkoutType.CrossFit);

            workout.Should().NotBeNull();
            workout.Name.Should().Be("Test");
            workout.Description.Should().Be("A Test Test Test");
            workout.MaxParticipantsCount.Should().Be(10);
            workout.StartsAtDate.Should().Be(startDate);
            workout.StartsAtTime.Should().Be(startTime);
            workout.Type.Should().Be(WorkoutType.CrossFit);
            workout.CurrentParticipantsCount.Should().Be(0);  
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void ConstructorShouldThrowExceptionIfNameIsInvalid(string name)
        {
            var constructor = () => new Workout(
                name,
                "A Test Test Test",
                10,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidDescriptions))]
        public void ConstructorShouldThrowExceptionIfDescriptionIsInvalid(string description)
        {
            var constructor = () => new Workout(
                "Test",
                description,
                10,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidMaxParticipantsCounts))]
        public void ConstructorShouldThrowExceptionIfMaxParticipantsCountIsInvalid(int maxParticipants)
        {
            var constructor = () => new Workout(
                "Test",
                "A Test Test Test",
                maxParticipants,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidStartDates))]
        public void ConstructorShouldThrowExceptionIfStartDateIsInvalid(DateTime startDate)
        {
            var constructor = () => new Workout(
                "Test",
                "A Test Test Test",
                10,
                startDate,
                TimeSpan.FromHours(9),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void IsClosedShouldReturnFalseIfWorkoutStartsAtIsLessThanDateTimeNow()
        {
            var workout = new Workout(
                "Test",
                "A Test Test Test",
                10,
                DateTime.Now,
                TimeSpan.FromHours(6),
                WorkoutType.CrossFit);

            workout.IsClosed().Should().BeTrue();
        }

        [Fact]
        public void IsClosedShouldReturnFalseIfWorkoutStartsAtIsGreaterThanDateTimeNow()
        {
            var workout = new Workout(
                "Test",
                "A Test Test Test",
                10,
                DateTime.Now.AddHours(1),
                TimeSpan.FromHours(6),
                WorkoutType.CrossFit);

            workout.IsClosed().Should().BeFalse();
        }

        [Fact]
        public void IsFullShouldReturnFalseIfCurrentParticipantsCountIsLowerThanMaxParticipantsCount()
        {
            var workout = A.Dummy<Workout>();

            workout.IsFull().Should().BeFalse();
        }

        [Fact]
        public void IsFullShouldReturnTrueIfCurrentParticipantsCountIsGreaterThanOrEqualToMaxParticipantsCount()
        {
            var workout = new Workout(
                "Test",
                "A Test Test Test",
                1,
                DateTime.Now.AddHours(1),
                TimeSpan.FromHours(6),
                WorkoutType.CrossFit);

            workout.IncrementParticipantsCount();

            workout.IsFull().Should().BeTrue();
        }
        
        [Fact]
        public void IncrementParticipantsCountShouldIncrementCurrentParticipantsCount()
        {
            var workout = A.Dummy<Workout>();

            workout.IncrementParticipantsCount();

            workout.CurrentParticipantsCount.Should().Be(1);
        }

        [Fact]
        public void DecrementParticipantsCountShouldDecrementCurrentParticipantsCount()
        {
            var workout = A.Dummy<Workout>();

            workout.DecrementParticipantsCount();

            workout.CurrentParticipantsCount.Should().Be(-1);
        }

        //[Fact]
        //public void AddParticipantShouldThrowExceptionIfWorkoutIsClosed()
        //{
        //    var workout = new Workout(
        //        "Test",
        //        "A Test Test Test",
        //        10,
        //        DateTime.Now,
        //        TimeSpan.FromHours(6),
        //        WorkoutType.CrossFit);

        //    var athlete = A.Dummy<Athlete>();

        //    var addParticipant = () => workout.AddParticipant(athlete);

        //    addParticipant.Should().Throw<WorkoutClosedException>();
        //}

        //[Fact]
        //public void AddParticipantShouldThrowExceptionIfWorkoutIsFull()
        //{
        //    var workout = new Workout(
        //        "Test",
        //        "A Test Test Test",
        //        1,
        //        DateTime.Now.AddHours(1),
        //        TimeSpan.FromHours(6),
        //        WorkoutType.CrossFit);

        //    var athleteOne = A.Dummy<Athlete>();
        //    workout.AddParticipant(athleteOne);

        //    var athleteTwo = A.Dummy<Athlete>();
        //    var addParticipant = () => workout.AddParticipant(athleteTwo);

        //    addParticipant.Should().Throw<WorkoutFullException>();
        //}

        //[Fact]
        //public void AddParticipantShouldThrowExceptionIfAthleteHasNotActiveMembership()
        //{
        //    var workout = A.Dummy<Workout>();

        //    var athlete = A.Dummy<Athlete>();
        //    athlete.DeleteMembership();

        //    var addParticipant = () => workout.AddParticipant(athlete);

        //    addParticipant.Should().Throw<MembershipExpiredException>();
        //}

        //[Fact]
        //public void AddParticipantShouldAddAthleteToWorkout()
        //{
        //    var workout = A.Dummy<Workout>();
        //    var athlete = A.Dummy<Athlete>();

        //    workout.AddParticipant(athlete);

        //    workout.Athletes.Should().Contain(athlete);
        //}

        //[Fact]
        //public void AddParticipantShouldIncrementCurrentParticipantsCount()
        //{
        //    var workout = A.Dummy<Workout>();
        //    var athlete = A.Dummy<Athlete>();

        //    workout.AddParticipant(athlete);

        //    workout.CurrentParticipantsCount.Should().Be(1);
        //}

        //[Fact]
        //public void RemoveParticipantShouldThrowExceptionIfAthleteIsNotPartOfTheWorkout()
        //{
        //    var workout = A.Dummy<Workout>();
        //    var athlete = A.Dummy<Athlete>();

        //    var removeParticipant = () => workout.RemoveParticipant(athlete);

        //    removeParticipant.Should().Throw<RemoveAthleteException>();
        //}

        //[Fact]
        //public void RemoveParticipantShouldRemoveAthleteFromWorkout()
        //{
        //    var workout = A.Dummy<Workout>();
        //    var athlete = A.Dummy<Athlete>();

        //    workout.AddParticipant(athlete);
        //    workout.RemoveParticipant(athlete);

        //    workout.Athletes.Should().NotContain(athlete);
        //}

        //[Fact]
        //public void RemoveParticipantShouldDecrementCurrentParticipantsCount()
        //{
        //    var workout = A.Dummy<Workout>();
        //    var athlete = A.Dummy<Athlete>();

        //    workout.AddParticipant(athlete);
        //    workout.RemoveParticipant(athlete);

        //    workout.CurrentParticipantsCount.Should().Be(0);
        //}

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void UpdateNameShouldThrowExceptionIfNameIsInvalid(string name)
        {
            var workout = A.Dummy<Workout>();

            var updateName = () => workout.UpdateName(name);

            updateName.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void UpdateNameShouldUpdateName()
        {
            var workout = A.Dummy<Workout>();
            var newName = "New Name";

            workout.UpdateName(newName);

            workout.Name.Should().Be(newName);
        }

        [Theory]
        [MemberData(nameof(InvalidDescriptions))]
        public void UpdateDescriptionShouldThrowExceptionIfDescriptionIsInvalid(string description)
        {
            var workout = A.Dummy<Workout>();

            var updateDescription = () => workout.UpdateDescription(description);

            updateDescription.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void UpdateDescriptionShouldUpdateDescription()
        {
            var workout = A.Dummy<Workout>();
            var newDescription = "New Description";

            workout.UpdateDescription(newDescription);

            workout.Description.Should().Be(newDescription);
        }

        [Theory]
        [MemberData(nameof(InvalidMaxParticipantsCounts))]
        public void UpdateMaxParticipantsCountShouldThrowExceptionIfMaxParticipantsCountIsInvalid(int maxParticipants)
        {
            var workout = A.Dummy<Workout>();

            var updateMaxParticipantsCount = () => workout.UpdateMaxParticipantsCount(maxParticipants);

            updateMaxParticipantsCount.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void UpdateMaxParticipantsCountShouldUpdateMaxParticipantsCount()
        {
            var workout = A.Dummy<Workout>();
            var newMaxParticipantsCount = 13;

            workout.UpdateMaxParticipantsCount(newMaxParticipantsCount);

            workout.MaxParticipantsCount.Should().Be(newMaxParticipantsCount);
        }

        [Theory]
        [MemberData(nameof(InvalidStartDates))]
        public void UpdateStartsAtDateShouldThrowExceptionIfStartDateIsInvalid(DateTime startDate)
        {
            var workout = A.Dummy<Workout>();

            var updateStartsAtDate = () => workout.UpdateStartsAtDate(startDate);

            updateStartsAtDate.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void UpdateStartsAtDateShouldUpdateStartDate()
        {
            var workout = A.Dummy<Workout>();
            var newStartDate = DateTime.Now.AddDays(2);

            workout.UpdateStartsAtDate(newStartDate);

            workout.StartsAtDate.Should().Be(newStartDate);
        }

        [Fact]
        public void UpdateStartsAtTimeShouldUpdateStartTime()
        {
            var workout = A.Dummy<Workout>();
            var newStartTime = TimeSpan.FromHours(10);

            workout.UpdateStartsAtTime(newStartTime);

            workout.StartsAtTime.Should().Be(newStartTime);
        }

        [Fact]
        public void UpdateTypeShouldUpdateType()
        {
            var workout = A.Dummy<Workout>();
            var newType = WorkoutType.Gymnastic;

            workout.UpdateType(newType);

            workout.Type.Should().Be(newType);
        }

        public static TheoryData<string> InvalidNames =>
            [
                null!,
                string.Empty,
                "a",
                new('a', 101)
            ];

        public static TheoryData<string> InvalidDescriptions =>
            [
                null!,
                string.Empty,
                "a",
                new('a', 501)
            ];

        public static TheoryData<int> InvalidMaxParticipantsCounts => [ -124421, 0, 16, 124124421 ];

        public static TheoryData<DateTime> InvalidStartDates => 
            [ 
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddMonths(-12),
                DateTime.Now.AddDays(8),
                DateTime.Now.AddMonths(12) 
            ];
    }
}
