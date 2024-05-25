using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;

public sealed class AdmissionGroupResponse
{
    public required Guid Id { get; init; }
    public required DateTime CreateTime { get; init; }
    public required AdmissionGroupStatus Status { get; set; }
}