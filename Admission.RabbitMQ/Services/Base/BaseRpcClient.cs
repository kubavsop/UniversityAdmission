using System.Collections.Concurrent;
using System.Text;
using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Admission.RabbitMQ.Services.Base;

public abstract class BaseRpcClient: IDisposable
{
    private readonly string _queueName;
    private readonly string _replyQueueName;
    private readonly IModel _channel;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<IRpcResponse?>> _callbackMapper = new();
    
    
    protected BaseRpcClient(string queueName, string replyQueueName ,IConnection connection)
    {
        _replyQueueName = replyQueueName;
        _queueName = queueName;
        _channel = connection.CreateModel();
        
        _channel.QueueDeclare(_replyQueueName, false, false, false, null);
        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += (model, ea) =>
        {
            if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;

            var body = ea.Body.Span;
            var response = Encoding.UTF8.GetString(body);
            
            var rpcRequest = JsonConvert.DeserializeObject<IRpcResponse>(response, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            
            tcs.TrySetResult(rpcRequest);
        };
        
        _channel.BasicConsume(consumer: consumer,
            queue: _replyQueueName,
            autoAck: true);
    }
    
    protected Task<IRpcResponse?> CallAsync<T>(T message, CancellationToken cancellationToken = default) where T: IRpcRequest<IRpcResponse?>
    {
        var props = _channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = _replyQueueName;
        
        var request = JsonConvert.SerializeObject(message, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        
        var messageBytes = Encoding.UTF8.GetBytes(request);
        var tcs = new TaskCompletionSource<IRpcResponse?>();
        _callbackMapper.TryAdd(correlationId, tcs);

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: _queueName,
            basicProperties: props,
            body: messageBytes);

        cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));
        return tcs.Task;
    }
    
    protected Result CheckError(IRpcResponse rpcResponse)
    {
        if (rpcResponse is RpcErrorResponse error) return new RpcException(error.Message);
        return Result.Success();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}