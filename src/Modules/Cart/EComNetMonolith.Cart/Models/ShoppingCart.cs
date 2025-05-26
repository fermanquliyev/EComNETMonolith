using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Cart.Models;

public class ShoppingCart: Aggregate<Guid>
{
    public string UserId { get; private set; } = default!;
    private readonly List<ShoppingCartItem> _items = new();
    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();
    public decimal TotalPrice => _items.Sum(item => item.Price * item.Quantity);

    public static ShoppingCart Create(Guid id, string userId)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId, nameof(userId));
        return new ShoppingCart
        {
            Id = id,
            UserId = userId
        };
    }

    public void AddItem(Guid productId, string productName, decimal price, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentException.ThrowIfNullOrEmpty(productName, nameof(productName));
        ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
        }

        var existingItem = _items.FirstOrDefault(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new ShoppingCartItem(Id, productId, productName, price, quantity);
            _items.Add(newItem);
        }
    }

    public void RemoveItem(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
        }
        var itemToRemove = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemToRemove != null)
        {
            _items.Remove(itemToRemove);
        }
    }

    public void UpdateItemQuantity(Guid productId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
        }
        var itemToUpdate = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemToUpdate != null)
        {
            itemToUpdate.Quantity = quantity;
            if (quantity <= 0)
            {
                _items.Remove(itemToUpdate);
            }
        }
    }
}
