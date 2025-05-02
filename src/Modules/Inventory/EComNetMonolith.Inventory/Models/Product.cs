﻿using EComNetMonolith.Inventory.Events;
using EComNetMonolith.Shared.DDD;

namespace EComNetMonolith.Inventory.Models
{
    public class Product: Aggregate<Guid>
    {
        public string Name { get; protected set; } = default!;
        public string Description { get; protected set; } = default!;
        public decimal Price { get; protected set; }
        public int Stock { get; protected set; }
        public string ImageUrl { get; protected set; } = default!;
        public List<string> Categories { get; protected set; } = new();

        public static Product Create(Guid id, string name, string description, decimal price, int stock, string imageUrl, List<string> categories)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

            var product = new Product
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                Stock = stock,
                ImageUrl = imageUrl,
                Categories = categories
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

            return product;
        }

        public void Update(string name, string description, decimal price, int stock, string imageUrl, List<string> categories)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
            Name = name;
            Description = description;
            if(price != Price)
            {
                AddDomainEvent(new ProductPriceChangedEvent(this,this.Price,price));
                Price = price;
            }
            Stock = stock;
            ImageUrl = imageUrl;
            Categories = categories;
        }
    }
}
