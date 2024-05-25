namespace Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

public sealed class EducationDocumentResponse
{
    public required Guid EducationDocumentId { get; init; }
    public required string Name { get; init; }
    public required Guid EducationDocumentTypeId { get; init; }
    public required IEnumerable<ScanRpcModel> Scans { get; init; }
}