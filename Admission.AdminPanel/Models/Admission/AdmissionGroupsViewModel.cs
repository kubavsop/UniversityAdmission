using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;

namespace Admission.AdminPanel.Models.Admission;

public sealed class AdmissionGroupsViewModel: IMapFrom<AdmissionGroupsResponse>
{
    public required IEnumerable<AdmissionGroupResponse> Groups { get; init; }
}