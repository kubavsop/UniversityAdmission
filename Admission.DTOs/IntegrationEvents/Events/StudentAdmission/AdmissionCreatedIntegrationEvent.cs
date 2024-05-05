using Admission.Domain.Common.Enums;

namespace Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

public sealed class AdmissionCreatedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public Guid? ManagerId { get; set; }
    public Guid ApplicantId { get; set; }
    public AdmissionStatus Status { get; set; }
    public Guid? FirstPriorityFacultyId { get; set; }
    public required string FirstPriorityFacultyName { get; set; }
}