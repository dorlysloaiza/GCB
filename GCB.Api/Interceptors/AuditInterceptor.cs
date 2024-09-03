using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

public class AuditInterceptor : DbCommandInterceptor
{
    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context as DbContext;
        if (context == null) return base.NonQueryExecuting(command, eventData, result);

        var entries = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            var entityName = entry.Entity.GetType().Name;
            var entityId = (Guid)entry.Property("Id").CurrentValue;
            var userName = (string)entry.Property("UserName").CurrentValue;
            var action = entry.State.ToString();
            var changes = JsonConvert.SerializeObject(entry.CurrentValues.ToObject());
            var originalValues = entry.State == EntityState.Added ? null : JsonConvert.SerializeObject(entry.OriginalValues.ToObject());
            var currentValues = entry.State == EntityState.Deleted ? null : JsonConvert.SerializeObject(entry.CurrentValues.ToObject());

            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                EntityName = entityName,
                EntityId = entityId,
                Action = action,
                Date = DateTime.UtcNow,
                UserName = userName,
                OriginalValues = originalValues,
                CurrentValues = currentValues
            };

            context.Set<AuditLog>().Add(log);
        }

        context.SaveChanges();

        return base.NonQueryExecuting(command, eventData, result);
    }
}
