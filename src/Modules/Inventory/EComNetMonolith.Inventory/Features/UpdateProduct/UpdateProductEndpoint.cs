using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Inventory.Features.CreateProduct;
using EComNetMonolith.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EComNetMonolith.Inventory.Features.UpdateProduct;

public record UpdateProductRequest(ProductDto Product);
public record UpdateProductResponse(Guid Id);
public class UpdateProductEndpoint : IEndpoint
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder.MapPut("/products",
            async ([FromBody] UpdateProductRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new UpdateProductCommand(request.Product);
                var result = await sender.Send(command, cancellationToken);
                return Results.Ok(new UpdateProductResponse(result.Id));
            })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
