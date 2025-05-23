namespace WodItEasy.Common.Domain.Models
{
    public interface IHaveDomainEvents
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
