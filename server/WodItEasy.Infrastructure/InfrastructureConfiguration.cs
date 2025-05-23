namespace WodItEasy.Infrastructure
{
    using System.Reflection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WodItEasy.Common.Infrastructure;
    using WodItEasy.Infrastructure.Persistence;

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
