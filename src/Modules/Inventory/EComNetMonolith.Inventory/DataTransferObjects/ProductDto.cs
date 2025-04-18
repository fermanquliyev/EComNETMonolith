namespace EComNetMonolith.Inventory.DataTransferObjects;

public record ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<string> Categories { get; init; }
    public string ImageUrl { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public ProductDto(Guid id, string name, List<string> categories, string imageUrl, string description, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Categories = categories;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        Stock = stock;
    }
}