using EComNetMonolith.Inventory.Models;
using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Inventory.Events
{
    public record ProductPriceChangedEvent(Product Product): IDomainEvent;
}
