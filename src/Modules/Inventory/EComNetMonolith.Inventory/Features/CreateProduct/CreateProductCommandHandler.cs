using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Inventory.Features.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string ImageUrl, string Description, decimal Price, int Stock): IRequest<CreateProductResponse>;

    public record CreateProductResponse(Guid Id);

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        public Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // TODO: Implement the command handler logic
            throw new NotImplementedException();
        }
    }
}
