namespace WodItEasy.Workouts.Infrastructure.Persistence
{
    using Common.Domain.Models;
    using Common.Infrastructure.Persistence;

    internal class WorkoutsDbInitializer(
        WorkoutDbContext data,
        IEnumerable<IInitialData> initialDataProviders)
        : DbInitializer(data, initialDataProviders)
    { }
}
