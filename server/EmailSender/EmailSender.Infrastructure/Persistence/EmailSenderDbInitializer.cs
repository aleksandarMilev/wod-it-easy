namespace WodItEasy.EmailSender.Infrastructure.Persistence
{
    using Common.Domain.Models;
    using Common.Infrastructure;

    internal class EmailSenderDbInitializer(
        EmailSenderDbContext data,
        IEnumerable<IInitialData> initialDataProviders)
        : DbInitializer(data, initialDataProviders)
    { }
}
