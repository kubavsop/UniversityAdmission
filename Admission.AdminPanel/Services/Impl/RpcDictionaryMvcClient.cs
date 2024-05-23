using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.RabbitMQ.Options;
using Admission.RabbitMQ.Services.Base;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Admission.AdminPanel.Services.Impl;

public sealed class RpcDictionaryMvcClient: BaseRpcClient, IRpcDictionaryMvcClient
{
    private const string QueueName = "DictionaryRpcQueue";

    
    public RpcDictionaryMvcClient(IOptions<RpcClientQueueNameOptions> replyQueueName, IConnection connection) : base(QueueName, replyQueueName.Value.Name, connection)
    {
    }


    public async Task<Result<FacultiesResponse>> GetFaculties(GetFacultiesRequest getFacultiesRequest)
    {
        var result = await CallAsync(getFacultiesRequest);
        if (result == null) return new RpcException("null response");

        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;

        return (result as FacultiesResponse)!;
    }
}