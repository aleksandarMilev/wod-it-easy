namespace WodItEasy.Domain.Common.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class EnumerationTest
    {
        private class TestEnumeration : Enumeration
        {
            public static readonly TestEnumeration One = new(1, "One");
            public static readonly TestEnumeration Two = new(2, "Two");

            public TestEnumeration(int value, string name) 
                : base(value, name) { }
        }

        [Fact]
        public void GetAllShouldReturnAllEnumerationValues()
        {
            var all = Enumeration.GetAll<TestEnumeration>();

            all.Should().Contain(new[] { TestEnumeration.One, TestEnumeration.Two });
        }

        [Fact]
        public void FromValueShouldReturnCorrectEnumeration()
        {
            var result = Enumeration.FromValue<TestEnumeration>(1);

            result.Should().Be(TestEnumeration.One);
        }

        [Fact]
        public void FromValueShouldThrowExceptionIfValueIsInvalid()
        {
            var fromValue = () => Enumeration.FromValue<TestEnumeration>(3);

            fromValue.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void FromNameShouldReturnCorrectEnumeration()
        {
            var result = Enumeration.FromName<TestEnumeration>("One");

            result.Should().Be(TestEnumeration.One);
        }

        [Fact]
        public void FromNameShouldThrowExceptionIfNameIsInvalid()
        {
            var fromName = () => Enumeration.FromName<TestEnumeration>("Three");

            fromName.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void NameFromValueShouldReturnCorrectName()
        {
            var name = Enumeration.NameFromValue<TestEnumeration>(1);

            name.Should().Be("One");
        }

        [Fact]
        public void HasValueShouldReturnTrueIfValueIsValid()
        {
            var result = Enumeration.HasValue<TestEnumeration>(1);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasValueShouldReturnFalseIfValueIsInvalid()
        {
            var result = Enumeration.HasValue<TestEnumeration>(3);

            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnTrueIfValuesAreSame()
        {
            var enum1 = TestEnumeration.One;
            var enum2 = TestEnumeration.One;

            enum1.Equals(enum2).Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfValuesAreDifferent()
        {
            var enum1 = TestEnumeration.One;
            var enum2 = TestEnumeration.Two;

            enum1.Equals(enum2).Should().BeFalse();
        }

        [Fact]
        public void GetHashCodeShouldReturnSameHashCodeForSameValues()
        {
            var enum1 = TestEnumeration.One;
            var enum2 = TestEnumeration.One;

            enum1.GetHashCode().Should().Be(enum2.GetHashCode());
        }

        [Fact]
        public void GetHashCodeShouldReturnDifferentHashCodeForDifferentValues()
        {
            var enum1 = TestEnumeration.One;
            var enum2 = TestEnumeration.Two;

            enum1.GetHashCode().Should().NotBe(enum2.GetHashCode());
        }

        [Fact]
        public void CompareToShouldReturnZeroForSameValues()
        {
            var enum1 = TestEnumeration.One;
            var enum2 = TestEnumeration.One;

            enum1.CompareTo(enum2).Should().Be(0);
        }

        [Fact]
        public void CompareToShouldNotReturnZeroForDifferentValues()
        {
            var enum1 = TestEnumeration.One;
            var enum2 = TestEnumeration.Two;

            enum1.CompareTo(enum2).Should().NotBe(0);
        }
    }
}