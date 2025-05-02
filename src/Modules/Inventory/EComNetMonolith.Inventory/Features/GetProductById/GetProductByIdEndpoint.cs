using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Inventory.Features.GetProductById;

public record GetProductByIdResponse(ProductDto Product);
public class GetProductByIdEndpoint: IEndpoint
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder.MapGet("/products/{id}",
            async ([FromRoute] Guid id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProductByIdQuery(id);
                var result = await sender.Send(query, cancellationToken);
                return Results.Ok(new GetProductByIdResponse(result.ProductDto));
            })
        .WithName("GetProductById")
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
