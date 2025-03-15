using EComNetMonolith.Products.Models;
using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Products.Events
{
    public record ProductCreatedEvent(Product Product): IDomainEvent;
}
