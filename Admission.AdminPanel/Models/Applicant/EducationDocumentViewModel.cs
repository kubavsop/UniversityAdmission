using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class EducationDocumentViewModel: DocumentViewModel, IMapFrom<EducationDocumentResponse>
{
    public required string Name { get; init; }
    public required Guid EducationDocumentTypeId { get; init; }
    public required IEnumerable<ScanRpcModel> Scans { get; init; }
}