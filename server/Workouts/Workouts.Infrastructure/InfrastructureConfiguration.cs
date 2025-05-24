namespace WodItEasy.Workouts.Infrastructure
{
    using System.Reflection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Common.Infrastructure;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCommonInfrastructure<WorkoutsDbContext>(
                    configuration,
                    Assembly.GetExecutingAssembly());
    }
}
