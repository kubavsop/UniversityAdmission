using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentType;

public sealed class GetDocumentTypeRequest: BaseDto, IRpcRequest<DocumentTypeResponse?>;