using MediatR;

namespace EComNetMonolith.Shared.DDD
{
    public interface IDomainEvent: INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public string EventName => GetType().AssemblyQualifiedName!;
    }
}
