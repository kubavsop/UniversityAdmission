using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.EducationLevel;
using Admission.DTOs.RpcModels.Faculty;

namespace Admission.DTOs.RpcModels.Program;

public sealed class ProgramResponse: BaseDto, IRpcResponse
{
    public required string Name { get; init; }
    
    public required string Code { get; init; }
    
    public required string Language { get; init; }
    
    public required string EducationForm { get; init; }
    
    public required FacultyResponse Faculty { get; init; }
    
    public required EducationLevelResponse EducationLevel { get; init; }
}