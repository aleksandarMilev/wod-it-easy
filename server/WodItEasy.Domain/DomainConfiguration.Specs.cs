namespace WodItEasy.Domain
{
    using Factories.Athlete;
    using Factories.Workout;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DomainConfigurationSpecs
    {
        [Fact]
        public void AddDomainShouldRegisterFactories()
        {
            var serviceCollection = new ServiceCollection();

            var services = serviceCollection
                .AddDomain()
                .BuildServiceProvider();

            services
                .GetService<IAthleteFactory>()
                .Should()
                .NotBeNull();

            services
                .GetService<IWorkoutFactory>()
                .Should()
                .NotBeNull();
        }
    }
}
