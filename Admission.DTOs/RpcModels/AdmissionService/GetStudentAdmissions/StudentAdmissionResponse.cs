using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;

public sealed class StudentAdmissionResponse
{
    public required Guid Id { get; init; }
    public required AdmissionStatus Status { get; init; }
    public required bool IsMyApplicant { get; init; }
}