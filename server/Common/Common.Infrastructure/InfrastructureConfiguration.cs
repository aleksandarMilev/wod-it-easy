namespace WodItEasy.Common.Infrastructure
{
    using System.Reflection;
    using System.Text;
    using Application.Contracts;
    using Application.Settings;
    using Events;
    using Extensions;
    using Hangfire;
    using Hangfire.SqlServer;
    using MassTransit;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Services;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddCommonInfrastructure<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = true)
            where TDbContext : DbContext
            => services
                .AddDatabase<TDbContext>(configuration)
                .AddTokenAuthentication(configuration)
                .AddRepositories(assembly)
                .AddHealth(
                    configuration,
                    databaseHealthChecks,
                    messagingHealthChecks);

        public static IServiceCollection AddCommonInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = true)
            => services
                .AddTokenAuthentication(configuration)
                .AddRepositories(assembly)
                .AddHealth(
                    configuration,
                    databaseHealthChecks,
                    messagingHealthChecks);

        public static async Task<IServiceCollection> AddEvents(
            this IServiceCollection services,
            IConfiguration configuration,
            bool usePolling = true,
            params Type[] consumers)
        {
            var messageQueueSettings = configuration.GetMessageQueueSettings();

            services.AddMassTransit(mt =>
            {
                foreach (var consumer in consumers)
                    mt.AddConsumer(consumer);

                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseDelayedRedelivery(r => r.Interval(5, TimeSpan.FromSeconds(10)));
                    cfg.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(10)));

                    cfg.Host(messageQueueSettings.Host, h =>
                    {
                        h.Username(messageQueueSettings.UserName);
                        h.Password(messageQueueSettings.Password);
                    });

                    foreach (var consumer in consumers)
                    {
                        cfg.ReceiveEndpoint(consumer.FullName!, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.ConfigureConsumer(context, consumer);
                        });
                    }
                });
                
                mt.AddHealthChecks();
            })
            .AddTransient<IEventPublisher, EventPublisher>();

            if (usePolling)
            {
                await services.AddHangfireDatabase(configuration);
                services
                    .AddHangfire(config => config
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(
                            configuration.GetCronJobsConnectionString(),
                            new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            }))
                    .AddHangfireServer()
                    .AddHostedService<MessagesHostedService>();
            }

            return services;
        }

        private static IServiceCollection AddDatabase<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TDbContext : DbContext
            => services
                .AddScoped<DbContext, TDbContext>()
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetDefaultConnectionString(),
                        sqlOptions => sqlOptions
                            .EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)
                            .MigrationsAssembly(
                                typeof(TDbContext).Assembly.FullName)));

        internal static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Assembly assembly)
            => services
                .Scan(scan => scan
                    .FromAssemblies(assembly)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        private static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
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

            services.AddHttpContextAccessor();
            return services;
        }

        private static async Task<IServiceCollection> AddHangfireDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetCronJobsConnectionString();
            var dbName = connectionString!
                .Split(";")[1]
                .Split("=")[1];

            using var connection = new SqlConnection(
                connectionString.Replace(dbName, "master"));

            await connection.OpenAsync();
            using var command = new SqlCommand(
                $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') create database [{dbName}];",
                connection);

            await command.ExecuteNonQueryAsync();

            return services;
        }

        private static IServiceCollection AddHealth(
            this IServiceCollection services,
            IConfiguration configuration,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = true)
        {
            var healthChecks = services.AddHealthChecks();

            if (databaseHealthChecks)
                healthChecks.AddSqlServer(configuration.GetDefaultConnectionString()!);

            if (messagingHealthChecks)
            {
                var messageQueueSettings = configuration.GetMessageQueueSettings();
                var messageQueueConnectionString =
                    $"amqp://{messageQueueSettings.UserName}:{messageQueueSettings.Password}@{messageQueueSettings.Host}/";

                healthChecks
                    .AddRabbitMQ(rabbitConnectionString: messageQueueConnectionString);
            }

            return services;
        }
    }
}
