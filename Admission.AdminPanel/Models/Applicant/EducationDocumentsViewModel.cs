using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class EducationDocumentsViewModel: IMapFrom<EducationDocumentsResponse>
{
    public required IEnumerable<EducationDocumentViewModel> DocumentResponses { get; init; }
    public required bool IsEditable { get; init; }
}