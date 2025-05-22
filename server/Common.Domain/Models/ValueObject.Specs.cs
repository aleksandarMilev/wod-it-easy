namespace WodItEasy.Common.Domain.Models.Tests
{
    using FluentAssertions;
    using Xunit;

    public class ValueObjectSpecs
    {
        private class TestValueObject(int value) : ValueObject
        {
            public int Value { get; } = value;
        }

        private class AnotherTestValueObject(int value) : ValueObject
        {
            public int Value { get; } = value;
        }


        [Fact]
        public void EqualsShouldReturnTrueIfValuesAreSame()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(1);

            obj1.Equals(obj2).Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfValuesAreDifferent()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(2);

            obj1.Equals(obj2).Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfArgumentIsNull()
        {
            var obj = new TestValueObject(1);

            obj.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTypesAreDifferent()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new AnotherTestValueObject(1);

            obj1.Equals(obj2).Should().BeFalse();
        }

        [Fact]
        public void GetHashCodeShouldReturnSameHashCodeIfValuesAreSame()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(1);

            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }

        [Fact]
        public void GetHashCodeShouldReturnDifferentHashCodeIfValuesAreDifferent()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(2);

            obj1.GetHashCode().Should().NotBe(obj2.GetHashCode());
        }

        [Fact]
        public void EqualityOperatorShouldReturnTrueIfValuesAreSame()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(1);

            (obj1 == obj2).Should().BeTrue();
        }

        [Fact]
        public void EqualityOperatorShouldReturnFalseIfValuesAreDifferent()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(2);

            (obj1 == obj2).Should().BeFalse();
        }

        [Fact]
        public void InEqualityOperatorShouldReturnFalseIfValuesAreSame()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(1);

            (obj1 != obj2).Should().BeFalse();
        }

        [Fact]
        public void InEqualityOperatorShouldReturnTrueIfValuesAreDifferent()
        {
            var obj1 = new TestValueObject(1);
            var obj2 = new TestValueObject(2);

            (obj1 != obj2).Should().BeTrue();
        }
    }
}