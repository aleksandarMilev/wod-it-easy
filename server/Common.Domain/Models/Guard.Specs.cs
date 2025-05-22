namespace WodItEasy.Common.Domain.Models.Tests
{
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class GuardSpecs
    {
        private class TestException : BaseDomainException { }

        [Fact]
        public void AgainstEmptyStringShouldThrowExceptionIfStringIsEmpty()
        {
            var againstEmptyString = () => Guard.AgainstEmptyString<TestException>("");

            againstEmptyString.Should().Throw<TestException>();
        }

        [Fact]
        public void AgainstEmptyStringShouldThrowExceptionIfStringIsNull()
        {
            var againstEmptyString = () => Guard.AgainstEmptyString<TestException>(null);

            againstEmptyString.Should().Throw<TestException>();
        }

        [Fact]
        public void AgainstEmptyStringShouldNotThrowIfStringIsValid()
        {
            var againstEmptyString = () => Guard.AgainstEmptyString<TestException>("Test");

            againstEmptyString.Should().NotThrow();
        }

        [Theory]
        [InlineData(5, 1, 10)]
        [InlineData(1, 1, 10)]
        [InlineData(10, 1, 10)]
        public void AgainstOutOfRangeShouldNotThrowIfNumberIsWithinRange(int number, int min, int max)
        {
            var againstOutOfRange = () => Guard.AgainstOutOfRange<TestException>(number, min, max);

            againstOutOfRange.Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 1, 10)]
        [InlineData(11, 1, 10)]
        public void AgainstOutOfRangeShouldThrowIfNumberIsOutOfRange(int number, int min, int max)
        {
            var againstOutOfRange = () => Guard.AgainstOutOfRange<TestException>(number, min, max);

            againstOutOfRange.Should().Throw<TestException>();
        }

        [Fact]
        public void ForValidUrlShouldNotThrowIfUrlIsValid()
        {
            var forValidUrl = () => Guard.ForValidUrl<TestException>("https://validUrl.com");

            forValidUrl.Should().NotThrow();
        }

        [Theory]
        [InlineData("")]
        [InlineData("invalid-url")]
        [InlineData("http//missing-colon.com")]
        public void ForValidUrlShouldThrowIfUrlIsInvalid(string url)
        {
            var forValidUrl = () => Guard.ForValidUrl<TestException>(url);

            forValidUrl.Should().Throw<TestException>();
        }

        [Fact]
        public void ForValidEmailShouldNotThrowIfEmailIsValid()
        {
            var forValidEmail = () => Guard.ForValidEmail<TestException>("valid@mail.com");

            forValidEmail.Should().NotThrow();
        }

        [Theory]
        [InlineData("")]
        [InlineData("invalid-email")]
        [InlineData("missing@domain")]
        [InlineData("missing.domain@")]
        public void ForValidEmailShouldThrowIfEmailIsInvalid(string email)
        {
            var forValidEmail = () => Guard.ForValidEmail<TestException>(email);

            forValidEmail.Should().Throw<TestException>();
        }

        [Fact]
        public void AgainstShouldThrowIfValuesAreEqual()
        {
            var against = () => Guard.Against<TestException>(42, 42);

            against.Should().Throw<TestException>();
        }

        [Fact]
        public void AgainstShouldNotThrowIfValuesAreNotEqual()
        {
            var against = () => Guard.Against<TestException>(42, 99);

            against.Should().NotThrow();
        }

        [Fact]
        public void ForStringLengthShouldNotThrowIfStringIsWithinRange()
        {
            var forStringLength = () => Guard.ForStringLength<TestException>("Test", 1, 10);

            forStringLength.Should().NotThrow();
        }

        [Fact]
        public void ForStringLengthShouldThrowIfStringIsTooShort()
        {
            var forStringLength = () => Guard.ForStringLength<TestException>("T", 2, 10);

            forStringLength.Should().Throw<TestException>();
        }

        [Fact]
        public void ForStringLengthShouldThrowIfStringIsTooLong()
        {
            var forStringLength = () => Guard.ForStringLength<TestException>("This is a very long string", 1, 10);

            forStringLength.Should().Throw<TestException>();
        }

        [Fact]
        public void AgainstOutOfRangeDateShouldThrowIfDateIsOutOfRange()
        {
            var date = DateTime.Now;
            var min = date.AddDays(1);
            var max = date.AddDays(5);

            var againstOutOfRange = () => Guard.AgainstOutOfRange<TestException>(date, min, max);

            againstOutOfRange.Should().Throw<TestException>();
        }

        [Fact]
        public void AgainstOutOfRangeDateShouldNotThrowIfDateIsWithinRange()
        {
            var date = DateTime.Now;
            var min = date.AddDays(-1);
            var max = date.AddDays(5);

            var againstOutOfRange = () => Guard.AgainstOutOfRange<TestException>(date, min, max);

            againstOutOfRange.Should().NotThrow();
        }
    }
}
