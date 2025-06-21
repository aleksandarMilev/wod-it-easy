namespace WodItEasy.Identity.Infrastructure
{
    using System.Reflection;
    using Application;
    using Common.Infrastructure;
    using Common.Infrastructure.Persistence;
    using JwtGenerator;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Services;

    public static class InfrastructureConfiguration
    {
        public static async Task<IServiceCollection> AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity()
                .AddCommonInfrastructure<IdentityDbContext>(
                    configuration,
                    Assembly.GetExecutingAssembly())
                .AddTransient<IDbInitializer, IdentityDbInitializer>();

            await services.AddEvents(
                  configuration,
                  usePolling: false,
                  consumers: []);

            return services;
        }

        private static IServiceCollection AddIdentity(
            this IServiceCollection services)
        {
            services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IJwtTokenGenerator, JwtTokenGenerator>()
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<IdentityDbContext>();

            return services;
        }
    }
}
