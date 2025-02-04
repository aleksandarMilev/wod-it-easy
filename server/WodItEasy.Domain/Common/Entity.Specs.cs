namespace WodItEasy.Domain.Common
{
    using FluentAssertions;
    using Xunit;

    public class EntitySpecs
    {
        private class TestEntity : Entity<int>
        {
            public TestEntity(int id)
                => this.Id = id;
        }

        private class AnotherEntity : Entity<int>
        {
            public AnotherEntity(int id)
                => this.Id = id;
        }

        [Fact]
        public void EqualsShouldReturnTrueIdIdsAreSame()
        {
            var id = 1;
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            entity1.Equals(entity2).Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnTrueIdIdsAreDifferent()
        {
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            entity1.Equals(entity2).Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfArgumentIsNull()
        {
            var entity = new TestEntity(1);

            entity.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTypesAreDifferent()
        {
            var entity1 = new TestEntity(1);
            var entity2 = new AnotherEntity(1);

            entity1.Equals(entity2).Should().BeFalse();
        }

        [Fact]
        public void EqualityOperatorShouldReturnTrueIfIdsAreSame()
        {
            var id = 1;
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            (entity1 == entity2).Should().BeTrue();
        }

        [Fact]
        public void EqualityOperatorShouldReturnFalseIfIdsAreDifferent()
        {
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            (entity1 == entity2).Should().BeFalse();
        }

        [Fact]
        public void InEqualityOperatorShouldReturnFalseIfIdsAreSame()
        {
            var id = 1;
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            (entity1 != entity2).Should().BeFalse();
        }

        [Fact]
        public void InEqualityOperatorShouldReturnTrueIfIdsAreDifferent()
        {
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            (entity1 != entity2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCodeShouldReturnSameHashCodeIfIdsAreSame()
        {
            var id = 1;
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        }

        [Fact]
        public void GetHashCodeShouldReturnDifferentHashCodeIfIdsAreDifferent()
        {
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
        }
    }
}
