using Admission.Domain.Common.Events;
using Admission.OutboxMessages.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Admission.OutboxMessages.Services.Impl;

public sealed class ProcessOutboxMessageService: IProcessOutboxMessageService
{
    private readonly ILogger<ProcessOutboxMessageService> _logger;
    private readonly IOutboxMessageDbContext _context;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessageService(IOutboxMessageDbContext context, IPublisher publisher, ILogger<ProcessOutboxMessageService> logger)
    {
        _context = context;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
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
                    _logger.LogCritical($"ERROR:The domain event with id={outboxMessage.Id} is null");
                }
                
                try
                { 
                    await _publisher.Publish(domainEvent);
                }
                catch (Exception e)
                {
                    _logger.LogError($"The domain event with id={outboxMessage.Id} didn't publish");
                    continue;
                }
                
                outboxMessage.ProcessedTime = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}