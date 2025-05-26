namespace WodItEasy.Profile.Infrastructure
{
    using System.Reflection;
    using Common.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCommonInfrastructure<ProfileDbContext>(
                    configuration,
                    Assembly.GetExecutingAssembly());
    }
}
