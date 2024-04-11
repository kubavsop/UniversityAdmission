namespace Admission.Infrastructure.Common.Outbox.BackgroundServices;

public interface IProcessOutboxMessageService
{
    Task DoWork(CancellationToken stoppingToken);
}