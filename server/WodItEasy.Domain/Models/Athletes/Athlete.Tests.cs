namespace WodItEasy.Domain.Models.Athletes
{
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class AthleteTests
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

        public static TheoryData<string> InvalidNames => 
            [
                null!,
                string.Empty,
                "a",
                new('a', 101)
            ];
    }
}
