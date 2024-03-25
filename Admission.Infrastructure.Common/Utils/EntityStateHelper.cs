using Admission.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Admission.Infrastructure.Common.Utils;

internal static class EntityStateHelper
{
    internal static void SetTimestamps(ChangeTracker changeTracker)
    {
        var now = DateTime.UtcNow;
        
        foreach (var entity in changeTracker.Entries<IBaseEntity>())
        {
            switch (entity.State)
            {
                case EntityState.Deleted:
                    entity.Entity.DeleteTime = now;
                    entity.State = EntityState.Modified;
                    break;
                case EntityState.Added:
                    entity.Entity.CreateTime = now;
                    break;
            }
        }
    }
}