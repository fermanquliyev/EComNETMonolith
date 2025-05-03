using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.DtataTransferObjects;
using EComNetMonolith.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EComNetMonolith.Inventory.Features.GetProducts;

public record GetProductsRequest([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] string? Search = null, [FromQuery] string Categories = "");
public class GetProductsResponse: PaginatedResult<ProductDto>
{
    public GetProductsResponse(int totalCount, IList<ProductDto> items, int pageNumber, int pageSize) : base(items, totalCount, pageNumber, pageSize)
    {
    }
}
internal class GetProductsEndpoint : IEndpoint
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder.MapGet("/products",
        async (
             [FromServices] ISender sender,
             [AsParameters] GetProductsRequest request,
             CancellationToken cancellationToken = default
             ) =>
             {
                 var query = new GetProductsQuery(request.PageNumber, request.PageSize, request.Search, request.Categories?.Split(',').ToList());
                 var result = await sender.Send(query, cancellationToken);
                 return Results.Ok(new GetProductsResponse(result.TotalCount, result.Items, request.PageNumber, request.PageSize));
             })
         .WithName("GetProducts")
         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Products")
         .WithDescription("Get Products");
    }
}
