using EComNetMonolith.Shared.DDD;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EComNetMonolith.Shared.Data.Interceptors
{
    public class EntityAuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILogger<EntityAuditInterceptor> logger;

        public EntityAuditInterceptor(
            ILogger<EntityAuditInterceptor> logger
            )
        {
            this.logger = logger;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AuditEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            AuditEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AuditEntities(DbContext? dbContext)
        {
            if (dbContext == null)
            {
                logger.LogWarning("DbContext is null. Skipping audit.");
                return;
            }
            var entries = dbContext.ChangeTracker.Entries<IEntity>();
            foreach (var entry in entries)
            {

                var now = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreationTime = now;
                    entry.Entity.CreatorUserId = "system"; // TODO: Get current user
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModificationTime = now;
                    entry.Entity.LastModifierUserId = "system"; // TODO: Get current user
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.DeletionTime = now;
                    entry.Entity.DeleterUserId = "system"; // TODO: Get current user
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
