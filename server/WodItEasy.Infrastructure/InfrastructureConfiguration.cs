namespace WodItEasy.Infrastructure
{
    using System;
    using Application.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Persistence.Repositories;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<WodItEasyDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found!"),
                        b => b.MigrationsAssembly(typeof(WodItEasyDbContext).Assembly.FullName));
                })
                .AddTransient(typeof(IRepository<>), typeof(DataRepository<>));
    }
}
