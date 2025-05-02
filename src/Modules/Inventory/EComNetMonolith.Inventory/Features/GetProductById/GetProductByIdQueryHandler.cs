using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Inventory.Exceptions;
using EComNetMonolith.Shared.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EComNetMonolith.Inventory.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResponse>;
public record GetProductByIdQueryResponse(ProductDto ProductDto);
public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
public class GetProductByIdQueryHandler: IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly InventoryDbContext inventoryDbContext;
    public GetProductByIdQueryHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await inventoryDbContext.Products.AsNoTracking().SingleOrDefaultAsync(x=>x.Id == query.Id, cancellationToken: cancellationToken) 
            ?? throw new ProductNotFoundException(query.Id);
        var productDt = new ProductDto(
            product.Id,
            product.Name,
            product.Categories,
            product.ImageUrl,
            product.Description,
            product.Price,
            product.Stock
        );
        return new GetProductByIdQueryResponse(productDt);
    }
}
