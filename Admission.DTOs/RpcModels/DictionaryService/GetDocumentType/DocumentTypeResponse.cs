using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DictionaryService.GetEducationLevel;

namespace Admission.DTOs.RpcModels.DictionaryService.GetDocumentType;

public sealed class DocumentTypeResponse: BaseDto, IRpcResponse
{
    public required string Name { get; init; }
    
    public required EducationLevelResponse EducationLevel { get; init; }
    
    public required IEnumerable<EducationLevelResponse> NextEducationLevels { get; init; }
}