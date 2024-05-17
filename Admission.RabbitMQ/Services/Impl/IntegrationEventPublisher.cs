using System.Text;
using Admission.DTOs.IntegrationEvents;
using Admission.RabbitMQ.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Admission.RabbitMQ.Services.Impl;

public sealed class IntegrationEventPublisher: IIntegrationEventPublisher, IDisposable
{
    private readonly IntegrationQueuesOptions _queuesOptions;
    private readonly IModel _channel;

    public IntegrationEventPublisher(IOptions<IntegrationQueuesOptions> queuesOptions, IConnection connection)
    {
        _queuesOptions = queuesOptions.Value;
        
        _channel = connection.CreateModel();
        
        _channel.ExchangeDeclare(_queuesOptions.ExchangeName, ExchangeType.Topic, false, false);
        
        foreach (var topicQueue in _queuesOptions.Queues)
        {
            _channel.QueueDeclare(topicQueue.Name, false, false, false, null);
            _channel.QueueBind(
                queue: topicQueue.Name,
                exchange: _queuesOptions.ExchangeName, 
                topicQueue.RoutingKey);
        }
    }

    public void Publish(IIntegrationEvent integrationEvent, string routingKey)
    {  
        var payload = JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        var body = Encoding.UTF8.GetBytes(payload);
        
        _channel.BasicPublish(exchange:_queuesOptions.ExchangeName, routingKey: routingKey, body: body);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}