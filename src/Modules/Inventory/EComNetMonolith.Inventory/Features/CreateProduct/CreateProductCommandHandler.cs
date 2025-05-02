using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.DataTransferObjects;
using EComNetMonolith.Inventory.Models;
using EComNetMonolith.Shared.CQRS;
using FluentValidation;

namespace EComNetMonolith.Inventory.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductCommandResponse>;

public record CreateProductCommandResponse(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.Product.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");
        RuleFor(x => x.Product.ImageUrl).NotEmpty().WithMessage("ImageUrl is required.");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("At least one category is required.");
    }
}

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly InventoryDbContext inventoryDbContext;

    public CreateProductCommandHandler(InventoryDbContext inventoryDbContext)
    {
        this.inventoryDbContext = inventoryDbContext;
    }
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productDto = command.Product;

        var product = Product.Create(
            Guid.CreateVersion7(),
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.Stock,
            productDto.ImageUrl,
            productDto.Categories
            );

        inventoryDbContext.Products.Add(product);
        await inventoryDbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResponse(product.Id);
    }
}
