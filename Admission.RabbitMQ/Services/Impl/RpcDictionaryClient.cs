using System.Collections.Concurrent;
using System.Text;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.DocumentType;
using Admission.DTOs.RpcModels.EducationLevel;
using Admission.DTOs.RpcModels.Faculty;
using Admission.DTOs.RpcModels.Program;
using Admission.RabbitMQ.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Admission.RabbitMQ.Services.Impl;

public sealed class RpcClient: IRpcDictionaryClient, IDisposable
{
    private const string QueueName = "DictionaryRpcQueue";
    private readonly RpcClientQueueNameOptions _replyQueueName;
    private readonly IModel _channel;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<IRpcResponse?>> _callbackMapper = new();

    public RpcClient(IOptions<RpcClientQueueNameOptions> queueName, IConnection connection)
    {
        _replyQueueName = queueName.Value;
        _channel = connection.CreateModel();
        
        _channel.QueueDeclare(_replyQueueName.Name, false, false, false, null);
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
            queue: _replyQueueName.Name,
            autoAck: true);
    }

    public async Task<FacultyResponse?> GetFacultyByIdAsync(Guid id)
    {
        var result = await CallAsync(new GetFacultyRequest { Id = id });
        return result as FacultyResponse;
    }

    public async Task<ProgramResponse?> GetProgramByIdAsync(Guid id)
    {
        var result = await CallAsync(new GetProgramRequest { Id = id });
        return result as ProgramResponse;
    }

    public async Task<EducationLevelResponse?> GetEducationLevelByIdAsync(int id)
    {
        var result = await CallAsync(new GetEducationLevelRequest { Id = id });
        return result as EducationLevelResponse;
    }

    public async Task<DocumentTypeResponse?> GetDocumentTypeByIdAsync(Guid id)
    {
        var result = await CallAsync(new GetDocumentTypeRequest { Id = id });
        return result as DocumentTypeResponse;
    }

    private Task<IRpcResponse?> CallAsync<T>(T message, CancellationToken cancellationToken = default) where T: IRpcRequest<IRpcResponse?>
    {
        var props = _channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = _replyQueueName.Name;
        
        var request = JsonConvert.SerializeObject(message, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        
        var messageBytes = Encoding.UTF8.GetBytes(request);
        var tcs = new TaskCompletionSource<IRpcResponse?>();
        _callbackMapper.TryAdd(correlationId, tcs);

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: QueueName,
            basicProperties: props,
            body: messageBytes);

        cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));
        return tcs.Task;
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}