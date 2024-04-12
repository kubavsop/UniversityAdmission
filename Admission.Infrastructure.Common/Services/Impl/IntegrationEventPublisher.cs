using System.Text;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Infrastructure.Common.Messaging.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Admission.Infrastructure.Common.Services.Impl;

public sealed class IntegrationEventPublisher: IIntegrationEventPublisher, IDisposable
{
    private readonly IntegrationQueuesOptions _queuesOptions;
    private readonly IModel _channel;

    public IntegrationEventPublisher(IOptions<IntegrationQueuesOptions> queuesOptions, IConnection connection)
    {
        _queuesOptions = queuesOptions.Value;
        
        _channel = connection.CreateModel();
        
        _channel.ExchangeDeclare(_queuesOptions.ExchangeName, ExchangeType.Fanout, false, false);
        
        foreach (var queueName in _queuesOptions.QueueNames)
        {
            _channel.QueueDeclare(queueName, false, false, false, null);
            _channel.QueueBind(
                queue: queueName,
                exchange: _queuesOptions.ExchangeName, 
                "");
        }
    }

    public void Publish(IIntegrationEvent integrationEvent)
    {  
        var payload = JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        var body = Encoding.UTF8.GetBytes(payload);
        
        _channel.BasicPublish(exchange:_queuesOptions.ExchangeName, routingKey: "", body: body);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}