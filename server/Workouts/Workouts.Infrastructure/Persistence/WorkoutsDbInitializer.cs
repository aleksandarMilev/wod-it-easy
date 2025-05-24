namespace WodItEasy.Workouts.Infrastructure.Persistence
{
    using Common.Domain.Models;
    using Common.Infrastructure;

    internal class WorkoutsDbInitializer(
        WorkoutDbContext data,
        IEnumerable<IInitialData> initialDataProviders)
        : DbInitializer(data, initialDataProviders)
    { }
}
