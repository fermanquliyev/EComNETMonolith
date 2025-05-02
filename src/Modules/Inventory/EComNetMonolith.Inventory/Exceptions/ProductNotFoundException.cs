using EComNetMonolith.Shared.Exceptions;

namespace EComNetMonolith.Inventory.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product", id)
    {
    }
}
