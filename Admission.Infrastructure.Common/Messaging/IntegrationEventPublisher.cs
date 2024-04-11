using System.Text;
using Admission.Application.Common.Messaging;
using Admission.Infrastructure.Common.Messaging.Settings;
using Admission.Infrastructure.Common.Messaging.Settings.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Admission.Infrastructure.Common.Messaging;

public sealed class IntegrationEventPublisher: IIntegrationEventPublisher, IDisposable
{
    private readonly IntegrationQueuesOptions _queuesOptions;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IntegrationEventPublisher(IOptions<IntegrationQueuesOptions> messageBrokerOptions, IConnection connection)
    {
        _connection = connection;
        _queuesOptions = messageBrokerOptions.Value;
        
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(_queuesOptions.ExchangeName, ExchangeType.Fanout, false, false);
        
        foreach (var queueName in _queuesOptions.QueueNames)
        {
            _channel.QueueDeclare(queueName, false, false, false);
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
        _connection.Dispose();
        _channel.Dispose();
    }
}