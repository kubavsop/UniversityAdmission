using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class EducationDocumentsViewModel: IMapFrom<EducationDocumentsResponse>
{
    public IEnumerable<EducationDocumentViewModel> DocumentResponses { get; init; } =
        new List<EducationDocumentViewModel>();
    public bool IsEditable { get; init; }
}