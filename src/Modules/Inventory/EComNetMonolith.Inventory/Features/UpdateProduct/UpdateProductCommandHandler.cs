using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.CQRS;

namespace EComNetMonolith.Inventory.Features.CreateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductCommandResponse>;

public record UpdateProductCommandResponse(Guid Id);

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly InventoryDbContext inventoryDbContext;

    public UpdateProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var productDto = command.Product;
        var product = await inventoryDbContext.Products.FindAsync([productDto.Id], cancellationToken: cancellationToken) ?? throw new Exception($"Product not found: {productDto.Id}");
        product.Update(
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.Stock,
            productDto.ImageUrl,
            productDto.Categories
        );
        inventoryDbContext.Products.Update(product);
        await inventoryDbContext.SaveChangesAsync(cancellationToken);
        return new UpdateProductCommandResponse(product.Id);
    }
}
