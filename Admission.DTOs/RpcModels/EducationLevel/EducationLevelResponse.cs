namespace Admission.DTOs.RpcModels.EducationLevel;

public sealed class EducationLevelResponse: IRpcResponse
{
    public Guid Id { get; init; }
    
    public int ExternalId { get; init; }
    
    public required string Name { get; init; }
}