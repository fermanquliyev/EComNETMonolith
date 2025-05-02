using EComNetMonolith.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EComNetMonolith.Inventory.Features.DeleteProduct
{
    public record DeleteProductResponse(Guid Id);
    public class DeleteProductEndpoint : IEndpoint
    {
        public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            return endpointRouteBuilder.MapPost("/products/{id}",
                async ([FromRoute] Guid id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command, cancellationToken);
                return Results.Accepted();
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status202Accepted)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
