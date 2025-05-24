namespace WodItEasy.Workouts.Infrastructure.Persistence
{
    using Common.Domain.Models;
    using Common.Infrastructure;

    internal class WorkoutsDbInitializer(
        WorkoutsDbContext data,
        IEnumerable<IInitialData> initialDataProviders)
        : DbInitializer(data, initialDataProviders)
    { }
}
