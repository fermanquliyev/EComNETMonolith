using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Inventory.Features.CreateProduct;
using EComNetMonolith.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EComNetMonolith.Inventory.Endpoints
{
    public record CreateProductRequest(ProductDto Product);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : IEndpoint
    {
        public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            return endpointRouteBuilder.MapPost("/products",
                async ([FromBody] CreateProductRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateProductCommand(request.Product);
                var result = await sender.Send(command, cancellationToken);
                return Results.Created($"/products/{result.Id}", new CreateProductResponse(result.Id));
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
