namespace EComNetMonolith.Shared.DDD
{
    public interface IAggregate<TPrimaryKey> : IEntity<TPrimaryKey>, IAggregate
    {
    }

    public interface IAggregate: IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
