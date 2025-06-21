namespace WodItEasy.Profile.Infrastructure
{
    using System.Reflection;
    using Application.Features.Profile;
    using Common.Infrastructure;
    using Common.Infrastructure.Persistence;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Repositories;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCommonInfrastructure<ProfileDbContext>(
                    configuration,
        Assembly.GetExecutingAssembly())
            .AddTransient<IProfileRepository, ProfileRepository>()
            .AddTransient<IDbInitializer, ProfileDbInitializer>();
    }
}
