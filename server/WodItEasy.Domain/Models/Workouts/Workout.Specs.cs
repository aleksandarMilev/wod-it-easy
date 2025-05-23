namespace WodItEasy.Domain.Models.Workouts
{
    using Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class WorkoutSpecs
    {
        [Fact]
        public void ConstructorShouldCreateValidWorkout()
        {
            var startDate = DateTime.Now.AddDays(1).Date;
            var startTime = TimeSpan.FromHours(9);
            var startsAt = startDate.Add(startTime);

            var workout = new Workout(
                "Test",
                "https://test-url.com/some-image",
                "A Test Test Test",
                10,
                startsAt,
                WorkoutType.CrossFit);

            workout.Should().NotBeNull();
            workout.Name.Should().Be("Test");
            workout.Description.Should().Be("A Test Test Test");
            workout.MaxParticipantsCount.Should().Be(10);
            workout.StartsAt.Should().Be(startsAt);
            workout.Type.Should().Be(WorkoutType.CrossFit);
            workout.CurrentParticipantsCount.Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void ConstructorShouldThrowExceptionIfNameIsInvalid(string name)
        {
            var constructor = () => new Workout(
                name,
                "https://test-url.com/some-image",
                "A Test Test Test",
                10,
                DateTime.Now.AddDays(1),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidImageUrls))]
        public void ConstructorShouldThrowExceptionIfImageUrlIsInvalid(string imageUrl)
        {
            var constructor = () => new Workout(
                "Test",
                imageUrl,
                "A Test Test Test",
                10,
                DateTime.Now.AddDays(1),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidDescriptions))]
        public void ConstructorShouldThrowExceptionIfDescriptionIsInvalid(string description)
        {
            var constructor = () => new Workout(
                "Test",
                "https://test-url.com/some-image",
                description,
                10,
                DateTime.Now.AddDays(1),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidMaxParticipantsCounts))]
        public void ConstructorShouldThrowExceptionIfMaxParticipantsCountIsInvalid(int maxParticipants)
        {
            var constructor = () => new Workout(
                "Test",
                "https://test-url.com/some-image",
                "A Test Test Test",
                maxParticipants,
                DateTime.Now.AddDays(1),
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Theory]
        [MemberData(nameof(InvalidStartDates))]
        public void ConstructorShouldThrowExceptionIfStartDateIsInvalid(DateTime startDate)
        {
            var constructor = () => new Workout(
                "Test",
                "https://test-url.com/some-image",
                "A Test Test Test",
                10,
                startDate,
                WorkoutType.CrossFit);

            constructor.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void IsClosedShouldReturnFalseIfWorkoutStartsAtIsGreaterThanDateTimeNow()
        {
            var workout = new Workout(
                "Test",
                "https://test-url.com/some-image",
                "A Test Test Test",
                10,
                DateTime.Now.AddHours(1),
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
                "https://test-url.com/some-image",
                "A Test Test Test",
                1,
                DateTime.Now.AddHours(1),
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
        [MemberData(nameof(InvalidImageUrls))]
        public void UpdateImageUrlShouldThrowExceptionIfNameIsInvalid(string imageUrl)
        {
            var workout = A.Dummy<Workout>();

            var updateImageUrl = () => workout.UpdateImageUrl(imageUrl);

            updateImageUrl.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void UpdateImageUrlShouldUpdateImageUrl()
        {
            var workout = A.Dummy<Workout>();
            var newImageUlr = "https://new-image-url.com";

            workout.UpdateImageUrl(newImageUlr);

            workout.ImageUrl.Should().Be(newImageUlr);
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

            var updateStartsAtDate = () => workout.UpdateStartsAt(startDate);

            updateStartsAtDate.Should().Throw<InvalidWorkoutException>();
        }

        [Fact]
        public void UpdateStartsAtDateShouldUpdateStartDate()
        {
            var workout = A.Dummy<Workout>();
            var newStartDate = DateTime.Now.AddDays(2);

            workout.UpdateStartsAt(newStartDate);

            workout.StartsAt.Should().Be(newStartDate);
        }

        [Fact]
        public void UpdateTypeShouldUpdateType()
        {
            var workout = A.Dummy<Workout>();
            var newType = WorkoutType.Gymnastic;

            workout.UpdateType(newType);

            workout.Type.Should().Be(newType);
        }

        public static TheoryData<string> InvalidNames
            => [ null!, string.Empty, "a",  new string('a', 101) ];

        public static TheoryData<string> InvalidImageUrls
            => [ null!, string.Empty, "a", new string('a', 2_049), "Invalid-Url" ];

        public static TheoryData<string> InvalidDescriptions
            => [ null!, string.Empty, "a", new string('a', 501) ];

        public static TheoryData<int> InvalidMaxParticipantsCounts
            => [ -124421, 0, 16, 124124421 ];

        public static TheoryData<DateTime> InvalidStartDates 
            => [
                    DateTime.Now.AddDays(-1), 
                    DateTime.Now.AddMonths(-12), 
                    DateTime.Now.AddDays(8), 
                    DateTime.Now.AddMonths(12)
                ];
    }
}
