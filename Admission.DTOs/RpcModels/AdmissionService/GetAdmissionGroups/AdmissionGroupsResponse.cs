namespace Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;

public sealed class AdmissionGroupsResponse: IRpcResponse
{
    public required IEnumerable<AdmissionGroupResponse> Groups { get; init; }
}