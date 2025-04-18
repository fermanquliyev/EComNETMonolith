using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Shared.CQRS;
using MediatR;

namespace EComNetMonolith.Inventory.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<Unit>;
public class DeleteProductCommandHandler: ICommandHandler<DeleteProductCommand>
{
    private readonly InventoryDbContext inventoryDbContext;
    public DeleteProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await inventoryDbContext.Products.FindAsync([command.Id],cancellationToken:cancellationToken) ?? throw new Exception($"Product not found: {command.Id}");
        inventoryDbContext.Products.Remove(product);
        await inventoryDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
