using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace EComNetMonolith.Inventory.Features.GetProducts;

public record GetProductsQuery(int PageNumber, int PageSize, string? Search = null, List<string>? Categories = default) : IQuery<GetProductsQueryResponse>;
public record GetProductsQueryResponse(int TotalCount, List<ProductDto> Products);
public class GetProductsQueryHandler: IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
{
    private readonly InventoryDbContext inventoryDbContext;
    public GetProductsQueryHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var productsQuery = inventoryDbContext.Products.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            productsQuery = productsQuery.Where(p => p.Name.Contains(query.Search) || p.Description.Contains(query.Search));
        }

        if (query.Categories != null && query.Categories.Count > 0)
        {
            productsQuery = productsQuery.Where(p => p.Categories.Any(c => query.Categories.Contains(c)));
        }

        var products = await productsQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        var totalCount = await productsQuery.CountAsync(cancellationToken);

        return new GetProductsQueryResponse
        (
            totalCount,
            products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Categories,
                p.ImageUrl,
                p.Description,
                p.Price,
                p.Stock
            )).ToList()
        );
    }
}
