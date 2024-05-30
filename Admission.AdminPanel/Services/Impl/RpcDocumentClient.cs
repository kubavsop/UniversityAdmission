using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.AddScan;
using Admission.DTOs.RpcModels.DocumentService.ChangeEducationDocument;
using Admission.DTOs.RpcModels.DocumentService.ChangePassport;
using Admission.DTOs.RpcModels.DocumentService.DeleteScan;
using Admission.DTOs.RpcModels.DocumentService.DownloadScan;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;
using Admission.RabbitMQ.Services.Base;
using RabbitMQ.Client;

namespace Admission.AdminPanel.Services.Impl;

public sealed class RpcDocumentClient: BaseRpcClient, IRpcDocumentClient
{
    private const string QueueName = "DocumentRpcQueue";
    private const string ReplyQueueName = "RpcDocumentClientAdminPanelQueue";
    
    public RpcDocumentClient(IConnection connection) : base(QueueName, ReplyQueueName, connection)
    {
    }

    public async Task<Result> AddScanAsync(AddScanRequest scanRequest)
    {
        var result = await CallAsync(scanRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> ChangeEducationDocumentAsync(ChangeEducationDocumentRequest educationDocumentRequest)
    {
        var result = await CallAsync(educationDocumentRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> ChangePassportAsync(ChangePassportRequest passportRequest)
    {
        var result = await CallAsync(passportRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> DeleteScanAsync(DeleteScanRequest scanRequest)
    {
        var result = await CallAsync(scanRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result<EducationDocumentsResponse>> GetEducationDocumentsAsync(GetEducationDocumentsRequest documentsRequest)
    {
        var result = await CallAsync(documentsRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as EducationDocumentsResponse)!;
    }

    public async Task<Result<PassportResponse>> GetPassportAsync(GetPassportRequest getPassportRequest)
    {
        var result = await CallAsync(getPassportRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as PassportResponse)!;
    }

    public async Task<Result<ScanRpcFullModelResponse>> DownloadFileAsync(DownloadScanRequest downloadScanRequest)
    {
        var result = await CallAsync(downloadScanRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as ScanRpcFullModelResponse)!;
    }
}