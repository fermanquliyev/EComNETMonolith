using MediatR;
using Microsoft.Extensions.Logging;

namespace EComNetMonolith.Inventory.Events.Handlers
{
    public class ProductCreatedEventHandler: INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> logger;

        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
        {
            this.logger = logger;
        }
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var product = notification.Product;
            logger.LogInformation("Product created: {ProductId}, {ProductName}", product.Id, product.Name);
            return Task.CompletedTask;
        }
    }
}
