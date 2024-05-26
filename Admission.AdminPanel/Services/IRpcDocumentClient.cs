using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.AddScan;
using Admission.DTOs.RpcModels.DocumentService.ChangeEducationDocument;
using Admission.DTOs.RpcModels.DocumentService.ChangePassport;
using Admission.DTOs.RpcModels.DocumentService.DeleteScan;
using Admission.DTOs.RpcModels.DocumentService.DownloadScan;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;

namespace Admission.AdminPanel.Services;

public interface IRpcDocumentClient
{
    Task<Result> AddScanAsync(AddScanRequest scanRequest);
    Task<Result> ChangeEducationDocumentAsync(ChangeEducationDocumentRequest educationDocumentRequest);
    Task<Result> ChangePassportAsync(ChangePassportRequest passportRequest);
    Task<Result> DeleteScanAsync(DeleteScanRequest scanRequest);
    Task<Result<EducationDocumentsResponse>> GetEducationDocumentsAsync(GetEducationDocumentsRequest documentsRequest);
    Task<Result<PassportResponse>> GetPassportAsync(GetPassportRequest getPassportRequest);
    Task<Result<ScanRpcFullModelResponse>> DownloadFileAsync(DownloadScanRequest downloadScanRequest);
}