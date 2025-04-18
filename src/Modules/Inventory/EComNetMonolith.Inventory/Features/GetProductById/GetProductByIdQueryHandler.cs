using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.CQRS;

namespace EComNetMonolith.Inventory.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;
public class GetProductByIdQueryHandler: IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly InventoryDbContext inventoryDbContext;
    public GetProductByIdQueryHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await inventoryDbContext.Products.FindAsync([ query.Id ], cancellationToken: cancellationToken);
        if (product == null)
        {
            throw new Exception($"Product not found: {query.Id}");
        }
        return new ProductDto(
            product.Id,
            product.Name,
            product.Categories,
            product.ImageUrl,
            product.Description,
            product.Price,
            product.Stock
        );
    }
}
