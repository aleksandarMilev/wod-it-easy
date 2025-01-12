namespace WodItEasy.Domain.Common
{
    using System.Collections.Generic;

    public interface IHaveDomainEvents
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
