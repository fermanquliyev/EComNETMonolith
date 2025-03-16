using EComNetMonolith.Inventory.Models;
using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Inventory.Events
{
    public record ProductCreatedEvent(Product Product): IDomainEvent;
}
