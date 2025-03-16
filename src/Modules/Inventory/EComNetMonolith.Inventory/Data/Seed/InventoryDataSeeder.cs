using EComNetMonolith.Shared.Data;

namespace EComNetMonolith.Inventory.Data.Seed
{
    public class InventoryDataSeeder(InventoryDbContext dbContext) : IDataSeeder
    {
        public void Seed()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(InitialInventoryData.Products);
                dbContext.SaveChanges();
            }
        }
    }
}
