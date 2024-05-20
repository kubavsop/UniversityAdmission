namespace Admission.DTOs.RpcModels.DictionaryService.GetEducationLevel;

public sealed class GetEducationLevelRequest : IRpcRequest<EducationLevelResponse?>
{
    public int Id { get; init; }
};