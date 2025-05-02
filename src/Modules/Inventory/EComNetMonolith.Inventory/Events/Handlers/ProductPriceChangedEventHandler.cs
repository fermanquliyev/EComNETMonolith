using MediatR;
using Microsoft.Extensions.Logging;

namespace EComNetMonolith.Inventory.Events.Handlers
{
    public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
    {
        private readonly ILogger<ProductPriceChangedEventHandler> logger;

        public ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
        {
            this.logger = logger;
        }
        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            var product = notification.Product;
            logger.LogInformation("Product price changed: {ProductId}, {ProductName}, {OldPrice} -> {NewPrice}", product.Id, product.Name, notification.OldPrice, notification.NewPrice);
            return Task.CompletedTask;
        }
    }
}
