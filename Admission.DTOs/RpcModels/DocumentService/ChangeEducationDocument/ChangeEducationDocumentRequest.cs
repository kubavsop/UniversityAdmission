using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.ChangeEducationDocument;

public class ChangeEducationDocumentRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid DocumentId { get; init; }
    
    public required string Name { get; init; }
    
    public required Guid EducationDocumentTypeId { get; init; }
}