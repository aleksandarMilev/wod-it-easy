namespace WodItEasy.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WodItEasy.Infrastructure.Persistence;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
            => services
                .AddDbContext<WodItEasyDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found!"),
                        b => b.MigrationsAssembly(typeof(WodItEasyDbContext).Assembly.FullName));  
                });
    }
}
