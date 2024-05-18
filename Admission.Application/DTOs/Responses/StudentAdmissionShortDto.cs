using Admission.Domain.Common.Enums;

namespace Admission.Application.DTOs.Responses;

public sealed class StudentAdmissionShortDto
{
    public Guid Id { get; init; }
    public DateTime CreateTime { get; init; }
    public AdmissionStatus Status { get; init; }
    public bool ExistManager { get; init; }
    public DateTime? ModifiedTime { get; init; }
}