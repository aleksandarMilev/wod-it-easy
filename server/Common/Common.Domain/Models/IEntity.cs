namespace WodItEasy.Common.Domain.Models
{
    using Events;

    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
