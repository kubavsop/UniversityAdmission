using Admission.Domain.Enums;

namespace Admission.Application.DTOs.Responses;

public sealed class AdmissionGroupDto
{
    public Guid Id { get; init; }
    public DateTime CreateTime { get; init; }
    public AdmissionGroupStatus Status { get; init; }
    public StudentAdmissionShortDto? ApplicantAdmission { get; init; }
}