using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

public sealed class GetEducationDocumentsRequest: AuthorizedRequest, IRpcRequest<EducationDocumentsResponse>
{
    public required Guid ApplicantId { get; init; }
}