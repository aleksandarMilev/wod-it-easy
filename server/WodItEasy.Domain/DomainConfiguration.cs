namespace WodItEasy.Domain
{
    using System.Reflection;
    using Common;
    using Factories;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Workouts;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .Scan(scan =>
                {
                    scan
                        .FromCallingAssembly()
                        .AddClasses(c => c.AssignableTo(typeof(IFactory<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime();
                })
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddTransient<IInitialData, WorkoutData>();
    }
}
