namespace WodItEasy.Workouts.Infrastructure
{
    using System.Reflection;
    using Application.Features.Athlete;
    using Application.Features.Participations;
    using Application.Features.Workouts;
    using Common.Infrastructure;
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
                .AddCommonInfrastructure<WorkoutDbContext>(
                    configuration,
                    Assembly.GetExecutingAssembly())
            .AddTransient<IWorkoutRepository, WorkoutRepository>()
            .AddTransient<IAthleteRepository, AthleteRepository>()
            .AddTransient<IParticipationRepository, ParticipationRepository>()
            .AddTransient<IDbInitializer, WorkoutsDbInitializer>();
    }
}
