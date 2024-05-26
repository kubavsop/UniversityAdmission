namespace Admission.DTOs.RpcModels.DictionaryService.GetDocumentTypes;

public sealed class DocumentTypeShortResponse
{
    public required Guid DocumentTypeId { get; init; }
    public required string DocumentTypeName { get; init; }
}