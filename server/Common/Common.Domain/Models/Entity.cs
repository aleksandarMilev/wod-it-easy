namespace WodItEasy.Common.Domain.Models
{
    public abstract class Entity<TId> : IHaveDomainEvents
        where TId : struct
    {
        private readonly List<IDomainEvent> events = [];

        protected Entity() { }

        public TId Id { get; protected set; } = default;

        public IReadOnlyCollection<IDomainEvent> DomainEvents 
            => this.events.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
            => this.events.Add(domainEvent);

        public void ClearDomainEvents() 
            => this.events.Clear();

        protected void RaiseEvent(IDomainEvent domainEvent) 
            => this.events.Add(domainEvent);

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other)
                return false;

            if (this.GetType() != other.GetType())
                return false;

            if (this.Id.Equals(default) || other.Id.Equals(default))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return this.Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
        {
            if (first is null && second is null)
                return true;

            if (first is null || second is null)
                return false;

            return first.Equals(second);
        }

        public override int GetHashCode()
            => (this.GetType().ToString() + this.Id).GetHashCode();

        public static bool operator !=(Entity<TId>? first, Entity<TId>? second)
            => !(first! == second!);
    }
}
