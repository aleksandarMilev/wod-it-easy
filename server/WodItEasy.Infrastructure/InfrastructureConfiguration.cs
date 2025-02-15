namespace WodItEasy.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Application;
    using Application.Contracts;
    using Application.Features.Identity;
    using Identity;
    using Identity.Jwt;
    using Identity.Roles;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Persistence;
    using Persistence.Interceptors;
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
            var repositoryInterfaces = GetRepositoriesInterfaces();
            var repositoryImplementations = GetRepositoriesImplementations();

            foreach (var repoInterface in repositoryInterfaces)
            {
                var genericInterface = repoInterface
                    .GetInterfaces()
                    .FirstOrDefault(i => 
                        i.IsGenericType && 
                        i.GetGenericTypeDefinition() == typeof(IRepository<>));

                if (genericInterface is not null)
                {
                    var matchingImplementation = repositoryImplementations
                        .FirstOrDefault(t =>
                            t.GetInterfaces()
                                .Any(i => i == genericInterface));

                    if (matchingImplementation is not null)
                    {
                        services.AddTransient(repoInterface, matchingImplementation);
                    }
                }
            }

            return services;
        }

        private static List<Type> GetRepositoriesInterfaces()
            => Assembly
                .GetAssembly(typeof(IRepository<>))
                ?.GetTypes()
                .Where(t =>
                    t.IsInterface &&
                    !t.IsGenericType &&
                    t.Name.Contains("Repository"))
                .ToList()
                ?? throw new InvalidOperationException("Could not load .Application assembly!");

        private static List<Type> GetRepositoriesImplementations()
            => Assembly
                .GetAssembly(typeof(DataRepository<>))
                ?.GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    !t.IsGenericType
                    && t.Name.Contains("Repository"))
                .ToList()
                ?? throw new InvalidOperationException("Could not load .Infrastructure assembly!");

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment
                .GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                ?? configuration.GetConnectionString("DefaultConnection");
                
            return services
                .AddDbContext<WodItEasyDbContext>(options =>
                {
                   options
                        .UseSqlServer(connectionString, sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(WodItEasyDbContext).Assembly.FullName);
                            sqlOptions.EnableRetryOnFailure();
                        });
                })
                .AddTransient<IInitializer, WodItEasyDbInitializer>()
                .AddTransient<IJwtTokenGeneratorService, JwtTokenGeneratorService>()
                .AddScoped<IRoleSeeder, RoleSeeder>()
                .AddScoped<PublishDomainEventInterceptor>();
        }

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
                    bearer.TokenValidationParameters = new TokenValidationParameters()
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
