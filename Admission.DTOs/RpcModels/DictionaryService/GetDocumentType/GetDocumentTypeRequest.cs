using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetDocumentType;

public sealed class GetDocumentTypeRequest: BaseDto, IRpcRequest<DocumentTypeResponse?>;