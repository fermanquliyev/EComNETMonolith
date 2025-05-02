using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Inventory.Exceptions;
using EComNetMonolith.Shared.CQRS;
using FluentValidation;

namespace EComNetMonolith.Inventory.Features.CreateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductCommandResponse>;

public record UpdateProductCommandResponse(Guid Id);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.Product.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");
        RuleFor(x => x.Product.ImageUrl).NotEmpty().WithMessage("ImageUrl is required.");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("At least one category is required.");
    }
}

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly InventoryDbContext inventoryDbContext;

    public UpdateProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var productDto = command.Product;
        var product = await inventoryDbContext.Products.FindAsync([productDto.Id], cancellationToken: cancellationToken) ?? throw new ProductNotFoundException(command.Product.Id);
        product.Update(
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.Stock,
            productDto.ImageUrl,
            productDto.Categories
        );
        inventoryDbContext.Products.Update(product);
        await inventoryDbContext.SaveChangesAsync(cancellationToken);
        return new UpdateProductCommandResponse(product.Id);
    }
}
