using Admission.Domain.Common.Entities;
using Admission.Infrastructure.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace Admission.Infrastructure.Common.Contexts;

public abstract class BaseContext: DbContext
{
    protected BaseContext(DbContextOptions options): base(options) {}
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        EntityStateHelper.SetTimestamps(ChangeTracker);
        return base.SaveChangesAsync(cancellationToken);
    }
}