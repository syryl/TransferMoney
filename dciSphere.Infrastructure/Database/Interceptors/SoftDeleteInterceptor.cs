using dciSphere.Abstraction.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Database.Interceptors;
internal sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
)
    {
        if (eventData.Context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity is ISoftDelete)
                        entry.CurrentValues[nameof(ISoftDelete.IsDeleted)] = false;
                    break;
                case EntityState.Deleted:
                    if (entry.Entity is ISoftDelete)
                    {
                        entry.State = EntityState.Modified;
                        eventData.Context.Entry(entry.Entity).CurrentValues[nameof(ISoftDelete.IsDeleted)] = true;
                    }
                    break;
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
