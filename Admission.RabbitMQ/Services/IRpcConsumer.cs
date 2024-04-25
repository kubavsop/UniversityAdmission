using Admission.DTOs.RpcModels;

namespace Admission.RabbitMQ.Services;

public interface IRpcConsumer
{
    Task<IRpcResponse?> Consume(IRpcRequest<IRpcResponse> rpcRequest);
}