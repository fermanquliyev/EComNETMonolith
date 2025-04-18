using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.CQRS;

namespace EComNetMonolith.Inventory.Features.CreateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(Guid Id);

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly InventoryDbContext inventoryDbContext;

    public UpdateProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var productDto = command.Product;
        var product = await inventoryDbContext.Products.FindAsync(productDto.Id);
        if (product == null)
        {
            throw new Exception($"Product not found: {productDto.Id}");
        }
        product.Update(
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.Stock,
            productDto.ImageUrl,
            productDto.Categories
        );
        inventoryDbContext.Products.Update(product);
        await inventoryDbContext.SaveChangesAsync();
        return new UpdateProductResponse(product.Id);
    }
}
