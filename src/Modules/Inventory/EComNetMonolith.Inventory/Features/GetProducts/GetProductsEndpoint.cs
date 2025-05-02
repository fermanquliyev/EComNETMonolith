using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EComNetMonolith.Inventory.Features.GetProducts;

public record GetProductsRequest(int PageNumber, int PageSize, string? Search = null, List<string>? Categories = default);
public record GetProductsResponse(int TotalCount, List<ProductDto> Products);
internal class GetProductsEndpoint : IEndpoint
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder.MapGet("/products",
        async (
             [FromServices] ISender sender,
             [FromQuery] int pageNumber = 1,
             [FromQuery] int pageSize = 10,
             [FromQuery] string? search = null,
             [FromQuery] string? categories = null,
             CancellationToken cancellationToken = default
             ) =>
             {
                 var categoriesList = categories?.Split(',').ToList();
                 var query = new GetProductsQuery(pageNumber, pageSize, search, categoriesList);
                 var result = await sender.Send(query, cancellationToken);
                 return Results.Ok(new GetProductsResponse(result.TotalCount, result.Products));
             })
         .WithName("GetProducts")
         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Products")
         .WithDescription("Get Products");
    }
}
