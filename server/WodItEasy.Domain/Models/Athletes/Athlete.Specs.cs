//namespace WodItEasy.Domain.Models.Athletes
//{
//    using Exceptions;
//    using FakeItEasy;
//    using FluentAssertions;
//    using Xunit;

//    public class AthleteSpecs
//    {
//        [Fact]
//        public void ConstructorShouldCreateAValidEntity()
//        {
//            var constructor = () => new Athlete("My Name");

//            constructor.Should().NotThrow<InvalidAthleteException>();
//        }

//        [Theory]
//        [MemberData(nameof(InvalidNames))]
//        public void ConstructorShouldThrowExceptionIfNameIsNotValid(string invalidName)
//        {
//            var constructor = () => new Athlete(invalidName);

//            constructor.Should().Throw<InvalidAthleteException>();
//        }

//        [Fact]
//        public void UpdateNameShouldSetNameProperty()
//        {
//            var athlete = A.Dummy<Athlete>();
//            var newName = "newName";

//            athlete.UpdateName(newName);

//            athlete.Name.Should().Be(newName);
//        }

//        [Theory]
//        [MemberData(nameof(InvalidNames))]
//        public void UpdateNameShouldThrowExceptionIfNameIsNotValid(string invalidName)
//        {
//            var athlete = A.Dummy<Athlete>();

//            var updateName = () => athlete.UpdateName(invalidName);

//            updateName.Should().Throw<InvalidAthleteException>();
//        }

//        public static TheoryData<string> InvalidNames => 
//            [
//                null!,
//                string.Empty,
//                "a",
//                new('a', 101)
//            ];
//    }
//}
