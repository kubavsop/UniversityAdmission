using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;

public sealed class AdmissionProgramsResponse: IRpcResponse
{
    public required IEnumerable<AdmissionProgramResponse> Programs { get; init; }
    public required bool IsEditable { get; init; }
}