﻿using System.Text;
using Admission.Application.Common.Messaging;
using Admission.Infrastructure.Common.Messaging.Options;
using Admission.Infrastructure.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Admission.Infrastructure.Common.BackgroundServices;

public sealed class IntegrationEventConsumerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IModel _channel;
    private readonly IntegrationConsumerQueueNameOptions _queueName;


    public IntegrationEventConsumerBackgroundService(
        IConnection connection, 
        IServiceProvider provider,
        IOptions<IntegrationConsumerQueueNameOptions> queuesOptions)
    {
        _provider = provider;
        _queueName = queuesOptions.Value;
        
        _channel = connection.CreateModel();
        
        _channel.QueueDeclare(_queueName.IntegrationConsumerQueueName, false, false, false, null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnIntegrationEventReceived;
        _channel.BasicConsume(_queueName.IntegrationConsumerQueueName, false, consumer);
        
        return Task.CompletedTask;
    }

    private void OnIntegrationEventReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        var body = Encoding.UTF8.GetString(eventArgs.Body.Span);

        var integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(body, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
        
        using var scope = _provider.CreateScope();

        var integrationEventConsumer = scope.ServiceProvider.GetRequiredService<IIntegrationEventConsumer>();

        integrationEventConsumer.Consume(integrationEvent ?? throw new ArgumentNullException("Bad integration event"));

        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }
}