using EComNetMonolith.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Inventory.Data.Seed
{
    static class InitialInventoryData
    {
        public static List<Product> Products => new List<Product>
            {
                Product.Create(new Guid("a3f1c9d2-4b5e-4d3a-8b1e-1a2b3c4d5e6f"), "Apple iPhone 13", "Latest model of Apple iPhone with A15 Bionic chip", 1000, 10, "iphone13.jpg", new List<string> { "Electronics" }),
                Product.Create(new Guid("b4e2d3c4-5f6a-4b7c-8d9e-2a3b4c5d6e7f"), "Samsung Galaxy S21", "Newest Samsung Galaxy phone with high-resolution camera", 800, 20, "galaxyS21.jpg", new List<string> { "Electronics" }),
                Product.Create(new Guid("c5f3e4d5-6a7b-4c8d-9e1f-3a4b5c6d7e8f"), "Sony WH-1000XM4", "Noise-cancelling over-ear headphones from Sony", 350, 30, "sonyWH1000XM4.jpg", new List<string> { "Electronics" }),
                Product.Create(new Guid("d5f3e4d5-6a7b-4d3a-9e1f-9a4b5c6d7e8f"), "Dell XPS 13", "High-performance laptop with Intel i7 processor", 1200, 40, "dellXPS13.jpg", new List<string> { "Computers" }),
                Product.Create(new Guid("a5f3e4d5-5f6a-4c8d-9e1f-7a4b5c6d7e8f"), "Apple Watch Series 7", "Latest Apple Watch with advanced health features", 400, 50, "appleWatch7.jpg", new List<string> { "Wearables" }),
            };
    }
}
