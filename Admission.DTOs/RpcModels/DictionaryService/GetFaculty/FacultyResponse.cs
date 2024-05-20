using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetFaculty;

public sealed class FacultyResponse: BaseDto, IRpcResponse
{
    public required string Name { get; init; }
}