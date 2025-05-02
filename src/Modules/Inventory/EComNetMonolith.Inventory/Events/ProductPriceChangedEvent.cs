using EComNetMonolith.Inventory.Models;
using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Inventory.Events
{
    public record ProductPriceChangedEvent(Product Product, decimal OldPrice, decimal NewPrice): IDomainEvent;
}
