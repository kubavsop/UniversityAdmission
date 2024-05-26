using Admission.DTOs.RpcModels.DictionaryService.GetDocumentType;

namespace Admission.DTOs.RpcModels.DictionaryService.GetDocumentTypes;

public class DocumentTypesResponse: IRpcResponse
{
    public required IEnumerable<DocumentTypeShortResponse> DocumentTypes;
}