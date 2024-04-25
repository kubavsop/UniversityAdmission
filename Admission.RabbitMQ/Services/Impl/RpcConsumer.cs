using Admission.DTOs.RpcModels;
using MediatR;

namespace Admission.RabbitMQ.Services.Impl;

public sealed class RpcConsumer: IRpcConsumer
{
    private readonly IMediator _mediator;

    public RpcConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IRpcResponse?> Consume(IRpcRequest<IRpcResponse> rpcRequest) => await _mediator.Send(rpcRequest);
}