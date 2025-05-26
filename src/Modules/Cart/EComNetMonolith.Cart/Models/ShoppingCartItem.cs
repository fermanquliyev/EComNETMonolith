using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Cart.Models;

public class ShoppingCartItem : Entity<Guid>
{
    public Guid ShoppingCartId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; internal set; }
    internal ShoppingCartItem(Guid cartId, Guid productId, string productName, decimal price, int quantity)
    {
        ShoppingCartId = cartId;
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}