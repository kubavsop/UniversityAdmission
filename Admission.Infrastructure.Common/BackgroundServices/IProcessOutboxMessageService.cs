namespace Admission.Infrastructure.Common.BackgroundServices;

public interface IProcessOutboxMessageService
{
    Task DoWork(CancellationToken stoppingToken);
}