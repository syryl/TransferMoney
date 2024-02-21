using dciSphere.Abstraction.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Database.Interceptors;
internal sealed class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var now = DateTime.Now;
        foreach (var entry in eventData.Context.ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues[nameof(AuditableEntity.ModifiedAt)] = now;
                    entry.CurrentValues[nameof(AuditableEntity.ModifiedBy)] = 1;
                    break;
                case EntityState.Added:
                    entry.CurrentValues[nameof(AuditableEntity.CreatedAt)] = now;
                    entry.CurrentValues[nameof(AuditableEntity.CreatedBy)] = 1;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
