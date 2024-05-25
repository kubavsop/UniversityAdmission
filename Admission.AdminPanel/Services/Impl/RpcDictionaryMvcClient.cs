using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;
using Admission.RabbitMQ.Services.Base;
using RabbitMQ.Client;

namespace Admission.AdminPanel.Services.Impl;

public sealed class RpcDictionaryMvcClient: BaseRpcClient, IRpcDictionaryMvcClient
{
    private const string QueueName = "DictionaryRpcQueue";
    private const string ReplyQueueName = "RpcDictionaryClientAdminPanelQueue";
    
    public RpcDictionaryMvcClient(IConnection connection) : base(QueueName, ReplyQueueName, connection)
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

    public async Task<Result<UpdateStatusResponse>> GetUpdateStatuses(GetUpdateStatusRequest getUpdateStatusRequest)
    {
        var result = await CallAsync(getUpdateStatusRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as UpdateStatusResponse)!;
    }
}