namespace Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

public sealed class EducationDocumentsResponse: IRpcResponse
{
    public required IEnumerable<EducationDocumentResponse> DocumentResponses { get; init; }
    public required bool IsEditable { get; init; }
}