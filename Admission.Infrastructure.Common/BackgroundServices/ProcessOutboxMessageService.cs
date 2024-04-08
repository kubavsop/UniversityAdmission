using Admission.Infrastructure.Common.Context;

namespace Admission.Infrastructure.Common.BackgroundServices;

public sealed class ProcessOutboxMessageService: IProcessOutboxMessageService
{
    private readonly IOutboxMessageDbContext _context;

    public ProcessOutboxMessageService(IOutboxMessageDbContext context)
    {
        _context = context;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("TODO");
            await Task.Delay(10000, stoppingToken);
        }
    }
}