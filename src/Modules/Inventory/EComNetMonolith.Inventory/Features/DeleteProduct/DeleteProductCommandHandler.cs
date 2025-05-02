using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.Exceptions;
using EComNetMonolith.Shared.CQRS;
using FluentValidation;
using MediatR;

namespace EComNetMonolith.Inventory.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<Unit>;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
public class DeleteProductCommandHandler: ICommandHandler<DeleteProductCommand>
{
    private readonly InventoryDbContext inventoryDbContext;
    public DeleteProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await inventoryDbContext.Products.FindAsync([command.Id],cancellationToken:cancellationToken) ?? throw new ProductNotFoundException(command.Id);
        inventoryDbContext.Products.Remove(product);
        await inventoryDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
