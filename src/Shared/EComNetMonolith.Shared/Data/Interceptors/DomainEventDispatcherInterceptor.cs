using EComNetMonolith.Shared.DDD;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Shared.Data.Interceptors
{
    public class DomainEventDispatcherInterceptor: SaveChangesInterceptor
    {
        private readonly IMediator mediator;
        private readonly ILogger<DomainEventDispatcherInterceptor> logger;

        public DomainEventDispatcherInterceptor(
            IMediator mediator,
            ILogger<DomainEventDispatcherInterceptor> logger
            )
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchEvents(DbContext? dbContext)
        {
            if(dbContext is null)
            {
                logger.LogWarning("DbContext is null, skipping domain event dispatching");
                return;
            }

            var aggregates = dbContext.ChangeTracker.Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity);

            var allDomainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();

            foreach (var aggregate in aggregates)
            {
                aggregate.ClearDomainEvents();
            }

            foreach (var domainEvent in allDomainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
