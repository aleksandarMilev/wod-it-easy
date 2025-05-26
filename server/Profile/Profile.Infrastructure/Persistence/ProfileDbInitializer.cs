namespace WodItEasy.Profile.Infrastructure.Persistence
{
    using Common.Domain.Models;
    using Common.Infrastructure;

    internal class ProfileDbInitializer(
        ProfileDbContext data,
        IEnumerable<IInitialData> initialDataProviders)
        : DbInitializer(data, initialDataProviders)
    { }
}
