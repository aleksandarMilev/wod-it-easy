namespace WodItEasy.Infrastructure.Persistence
{
    using WodItEasy.Common.Domain.Models;
    using WodItEasy.Common.Infrastructure;

    internal class WorkoutsDbInitializer(
        WorkoutsDbContext data,
        IEnumerable<IInitialData> initialDataProviders)
        : DbInitializer(data, initialDataProviders)
    { }
}
