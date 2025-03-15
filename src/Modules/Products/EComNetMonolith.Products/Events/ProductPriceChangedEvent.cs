using EComNetMonolith.Products.Models;
using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Products.Events
{
    public record ProductPriceChangedEvent(Product Product): IDomainEvent;
}
