namespace Admission.DTOs.RpcModels.EducationLevel;

public sealed class EducationLevelResponse: IRpcResponse
{
    public int Id { get; init; }
    
    public required string Name { get; init; }
}