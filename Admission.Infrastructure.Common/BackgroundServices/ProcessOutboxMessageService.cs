using Admission.Domain.Common.Events;
using Admission.Infrastructure.Common.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Admission.Infrastructure.Common.BackgroundServices;

public sealed class ProcessOutboxMessageService: IProcessOutboxMessageService
{
    private readonly IOutboxMessageDbContext _context;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessageService(IOutboxMessageDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("DoWork");
            var messages = await _context
                .OutboxMessages
                .Where(m => m.ProcessedTime == null)
                .Take(30)
                .ToListAsync(stoppingToken);

            foreach (var outboxMessage in messages)
            {
                IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                
                if (domainEvent is null)
                {
                    //TODO
                    continue;
                }

                try
                { 
                    await _publisher.Publish(domainEvent, stoppingToken);
                }
                catch (Exception e)
                {
                    //TODO
                    continue;
                }
                
                outboxMessage.ProcessedTime = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}