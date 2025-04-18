using EComNetMonolith.Shared.CQRS;

namespace EComNetMonolith.Inventory.Features.CreateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string ImageUrl, string Description, decimal Price, int Stock) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(Guid Id);

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement the command handler logic
        throw new NotImplementedException();
    }
}
