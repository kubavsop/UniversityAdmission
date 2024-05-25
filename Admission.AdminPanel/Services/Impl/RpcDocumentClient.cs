using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DocumentService.AddScan;
using Admission.DTOs.RpcModels.DocumentService.ChangeEducationDocument;
using Admission.DTOs.RpcModels.DocumentService.ChangePassport;
using Admission.DTOs.RpcModels.DocumentService.DeleteScan;
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

    public Task<Result> AddScanAsync(AddScanRequest scanRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangeEducationDocumentAsync(ChangeEducationDocumentRequest educationDocumentRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangePassportAsync(ChangePassportRequest passportRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteScanAsync(DeleteScanRequest scanRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<EducationDocumentsResponse>> GetEducationDocumentsAsync(GetEducationDocumentsRequest documentsRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PassportResponse>> GetPassportResponseAsync(GetPassportRequest getPassportRequest)
    {
        throw new NotImplementedException();
    }
}