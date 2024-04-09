using Admission.Infrastructure.Common.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Admission.Infrastructure.Common.Context;

public interface IOutboxMessageDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}