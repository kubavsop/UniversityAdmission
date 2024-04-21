namespace Admission.OutboxMessages.Services;

public interface IProcessOutboxMessageService
{
    Task DoWork(CancellationToken stoppingToken);
}