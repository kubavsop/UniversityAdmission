namespace Admission.DTOs.RpcModels.AdmissionService;

public sealed class ChangeProgramModel
{
    public required Guid Id { get; init; }
    public required int Priority { get; init; }
}