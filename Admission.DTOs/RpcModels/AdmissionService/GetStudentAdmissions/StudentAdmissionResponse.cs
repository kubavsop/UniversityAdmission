using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;

public sealed class StudentAdmissionResponse: IRpcResponse
{
    public required Guid AdmissionId { get; init; }
    public required AdmissionStatus Status { get; init; }
    public required bool IsMyApplicant { get; init; }
    public required bool ExistManager { get; init; }
    public required Guid ApplicantId { get; init; }
    public required string ApplicantEmail { get; init; }
    public bool IsEditable { get; set; }
    public string? ManagerName { get; init; }
    public required Guid? ManagerId { get; init; }
}