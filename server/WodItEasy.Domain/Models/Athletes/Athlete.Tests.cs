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
            var constructor = () => new Athlete("My Name", "my@mail.com", "+359888888888");

            constructor.Should().NotThrow<InvalidAthleteException>();
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void ConstructorShouldThrowExceptionIfNameIsNotValid(string invalidName)
        {
            var constructor = () => new Athlete(invalidName, "my@mail.com", "+359888888888");

            constructor.Should().Throw<InvalidAthleteException>();
        }

        [Theory]
        [MemberData(nameof(InvalidEmails))]
        public void ConstructorShouldThrowExceptionIfEmailIsNotValid(string invalidEmail)
        {
            var constructor = () => new Athlete("My Name", invalidEmail, "+359888888888");

            constructor.Should().Throw<InvalidAthleteException>();
        }

        public static TheoryData<string> InvalidNames => 
            [
                null!,
                string.Empty,
                "a",
                new('a', 101)
            ];

        public static TheoryData<string> InvalidEmails =>
            [
                null!,
                string.Empty,
                "a",
                new string('a', 101),
                "invalidemail",
                "user@.com",
                "user@@example.com",
                "user@exam!ple.com",
                "user @example.com",
                "user@ex@mple.com"
            ];
    }
}
