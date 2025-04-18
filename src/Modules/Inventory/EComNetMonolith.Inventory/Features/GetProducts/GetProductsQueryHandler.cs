using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace EComNetMonolith.Inventory.Features.GetProducts;

public record GetProductsQuery(int PageNumber, int PageSize, string? search = null, List<string>? categories = default) : IQuery<GetProductsQueryResponse>;
public class GetProductsQueryResponse
{
    public int TotalCount { get; set; }
    public List<ProductDto> Products { get; set; } = [];
}
public class GetProductsQueryHandler: IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
{
    private readonly InventoryDbContext inventoryDbContext;
    public GetProductsQueryHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var productsQuery = inventoryDbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(query.search))
        {
            productsQuery = productsQuery.Where(p => p.Name.Contains(query.search));
        }

        if (query.categories != null && query.categories.Count > 0)
        {
            productsQuery = productsQuery.Where(p => p.Categories.Any(c => query.categories.Contains(c)));
        }

        var products = await productsQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        var totalCount = await productsQuery.CountAsync(cancellationToken);

        return new GetProductsQueryResponse
        {
            TotalCount = totalCount,
            Products = products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Categories,
                p.ImageUrl,
                p.Description,
                p.Price,
                p.Stock
            )).ToList()
        };
    }
}
