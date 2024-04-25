using System.Text;
using Admission.DTOs.RpcModels;
using Admission.RabbitMQ.Options;
using Admission.RabbitMQ.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Admission.RabbitMQ.BackgroundServices;

public sealed class RpcConsumerBackgroundService: BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IModel _channel;
    private readonly RpcConsumerQueueNameOptions _queueName;
    private readonly ILogger<RpcConsumerBackgroundService> _logger;

    public RpcConsumerBackgroundService(
        IConnection connection,
        IServiceProvider provider,
        IOptions<RpcConsumerQueueNameOptions> queueOptions, 
        ILogger<RpcConsumerBackgroundService> logger)
    {
        _provider = provider;
        _logger = logger;
        _queueName = queueOptions.Value;

        _channel = connection.CreateModel();
        
        _channel.QueueDeclare(_queueName.Name, false, false, false, null);
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnReceived;
        _channel.BasicConsume(_queueName.Name, false, consumer);
        
        return Task.CompletedTask;
    }

    private async void OnReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        var props = eventArgs.BasicProperties;
        var replyProps = _channel.CreateBasicProperties();
        replyProps.CorrelationId = props.CorrelationId;
        
        var payload = string.Empty;
        try
        {
            var body = Encoding.UTF8.GetString(eventArgs.Body.Span);
            var rpcRequest = JsonConvert.DeserializeObject<IRpcRequest<IRpcResponse>>(body, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            using var scope = _provider.CreateScope();

            var rpcConsumer = scope.ServiceProvider.GetRequiredService<IRpcConsumer>();
            var rpcResponse = await rpcConsumer.Consume(rpcRequest!);

            payload = JsonConvert.SerializeObject(rpcResponse, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ERROR: Rpc consumer failed: {e.Message}");
        }
        finally
        {
            var responseBytes = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange: string.Empty,
                routingKey: props.ReplyTo,
                basicProperties: replyProps,
                body: responseBytes );
        
            _channel.BasicAck(eventArgs.DeliveryTag, false);   
        }
    }
    
    public override void Dispose()
    {
        _channel.Dispose();
        base.Dispose();
    }
}