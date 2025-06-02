namespace WodItEasy.Workouts.Domain.Models.Athletes
{
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class AthleteSpecs
    {
        [Fact]
        public void ConstructorShouldCreateAValidEntity()
        {
            var constructor = () => new Athlete("Some Name", "userId");

            constructor.Should().NotThrow<InvalidAthleteException>();
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void ConstructorShouldThrowExceptionIfNameIsNotValid(string invalidName)
        {
            var constructor = () => new Athlete(invalidName, "userId");

            constructor.Should().Throw<InvalidAthleteException>();
        }

        [Fact]
        public void UpdateNameShouldSetNameProperty()
        {
            var athlete = new Athlete("Old Name", "userId");
            var newName = "New Name";

            athlete.UpdateName(newName);

            athlete.Name.Should().Be(newName);
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void UpdateNameShouldThrowExceptionIfNameIsNotValid(string invalidName)
        {
            var athlete = new Athlete("Some Name", "userId");

            var updateName = () => athlete.UpdateName(invalidName);

            updateName.Should().Throw<InvalidAthleteException>();
        }

        [Fact]
        public void AthleteShouldHaveUserId()
        {
            var userId = "userId";
            var athlete = new Athlete("Some Name", userId);

            athlete.UserId.Should().Be(userId);
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
