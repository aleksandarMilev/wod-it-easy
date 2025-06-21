namespace WodItEasy.Common.Infrastructure.Events
{
    using Domain.Events;

    public interface IEventPublisher
    {
        Task Publish(IDomainEvent domainEvent);

        Task Publish<TDomainEvent>(
            TDomainEvent domainEvent,
            Type domainEventType);
    }
}