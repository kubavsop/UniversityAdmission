namespace Admission.Infrastructure.Common.Services;

public interface IProcessOutboxMessageService
{
    Task DoWork(CancellationToken stoppingToken);
}