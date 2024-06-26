﻿using Admission.OutboxMessages.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Admission.OutboxMessages.BackgroundServices;

public sealed class OutboxMessageProcessorService: BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OutboxMessageProcessorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }
    
    private async Task DoWork(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var service = 
                scope.ServiceProvider
                    .GetRequiredService<IProcessOutboxMessageService>();

            await service.DoWork(stoppingToken);
        }
    }
}