namespace WodItEasy.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Application;
    using Application.Contracts;
    using Application.Features.Identity;
    using Identity;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Persistence;
    using Persistence.Repositories;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services
                .RegisterRepositoriesWithTransientLifetime()
                .AddDatabase(configuration)
                .AddIdentity(configuration);

        private static IServiceCollection RegisterRepositoriesWithTransientLifetime(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.GetAssembly(typeof(IRepository<>));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(DataRepository<>));

            if (applicationAssembly is null || infrastructureAssembly is null)
            {
                throw new InvalidOperationException("Could not load required assemblies!");
            }

            var repositoryInterfaces = applicationAssembly
                .GetTypes()
                .Where(t => 
                    t.IsInterface && 
                    !t.IsGenericType &&
                    t.Name.Contains("Repository"))
                .ToList();

            var repositoryImplementations = infrastructureAssembly
                .GetTypes()
                .Where(t => 
                    t.IsClass && 
                    !t.IsAbstract && 
                    !t.IsGenericType 
                    && t.Name.Contains("Repository"))
                .ToList();

            foreach (var repoInterface in repositoryInterfaces)
            {
                var genericInterface = repoInterface
                    .GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>));

                if (genericInterface is not null)
                {
                    var matchingImplementation = repositoryImplementations
                        .FirstOrDefault(t => 
                        {
                            return t
                                .GetInterfaces()
                                .Any(i =>
                                {
                                    return i
                                        .IsGenericType && 
                                        i.GetGenericTypeDefinition() == genericInterface.GetGenericTypeDefinition();
                                });
                        });

                    if (matchingImplementation is not null)
                    {
                        services.AddTransient(repoInterface, matchingImplementation);
                    }
                }
            }

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<WodItEasyDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(WodItEasyDbContext).Assembly.FullName));
                })
                .AddTransient<IInitializer, WodItEasyDbInitializer>()
                .AddTransient<IJwtTokenGeneratorSerivce, JwtTokenGeneratorService>();

        private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<WodItEasyDbContext>();

            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            var key = Encoding.ASCII.GetBytes(secret!);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
