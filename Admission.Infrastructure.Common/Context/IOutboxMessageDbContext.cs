using Admission.Infrastructure.Common.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace Admission.Infrastructure.Common.Context;

public interface IOutboxMessageDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}