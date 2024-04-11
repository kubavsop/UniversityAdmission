using Microsoft.EntityFrameworkCore;

namespace Admission.Infrastructure.Common.Context;

public interface IOutboxMessageDbContext
{
    DbSet<OutboxMessage.OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}