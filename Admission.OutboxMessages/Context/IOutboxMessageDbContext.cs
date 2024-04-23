using Admission.OutboxMessages.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace Admission.OutboxMessages.Context;

public interface IOutboxMessageDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}