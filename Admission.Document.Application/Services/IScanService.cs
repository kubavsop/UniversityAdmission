using Admission.Application.Common.Result;
using Admission.Document.Application.DTOs.Responses;

namespace Admission.Document.Application.Services;

public interface IScanService
{
    Task<Result<FileResponse>> GetScanAsync(Guid userId, Guid fileId);
    Task<Result> DeleteScanAsync(Guid userId, Guid fileId);
}