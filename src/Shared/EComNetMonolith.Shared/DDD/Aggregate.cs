namespace EComNetMonolith.Shared.DDD
{
    public abstract class Aggregate<TPrimaryKey>: Entity<TPrimaryKey>, IAggregate<TPrimaryKey>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
