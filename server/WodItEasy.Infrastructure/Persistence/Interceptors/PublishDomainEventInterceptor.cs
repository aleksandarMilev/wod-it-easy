namespace WodItEasy.Infrastructure.Persistence.Interceptors
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using WodItEasy.Common.Domain.Models;

    public class PublishDomainEventInterceptor(
        IPublisher mediator)
        : SaveChangesInterceptor
    {
        private readonly IPublisher mediator = mediator;

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            this
                .PublishDomainEvents(eventData.Context)
                .GetAwaiter()
                .GetResult();

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await this.PublishDomainEvents(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishDomainEvents(DbContext? data)
        {
            if (data is null)
            {
                return;
            }

            var domainEventsEntities = data
                .ChangeTracker
                .Entries<IHaveDomainEvents>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = domainEventsEntities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            domainEventsEntities.ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await this.mediator.Publish(domainEvent);
            }
        }
    }
}
