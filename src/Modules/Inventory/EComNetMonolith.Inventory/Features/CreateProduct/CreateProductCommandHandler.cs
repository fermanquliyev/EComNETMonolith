using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Inventory.Models;
using EComNetMonolith.Shared.CQRS;

namespace EComNetMonolith.Inventory.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResponse>;

public record CreateProductResponse(Guid Id);

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly InventoryDbContext inventoryDbContext;

    public CreateProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productDto = command.Product;

        var product = Product.Create(
            Guid.CreateVersion7(),
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.Stock,
            productDto.ImageUrl,
            productDto.Categories
            );

        inventoryDbContext.Products.Add(product);
        await inventoryDbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResponse(product.Id);
    }
}
