namespace Admission.DTOs.RpcModels.EducationLevel;

public sealed class GetEducationLevelRequest : IRpcRequest<EducationLevelResponse?>
{
    public int Id { get; init; }
};