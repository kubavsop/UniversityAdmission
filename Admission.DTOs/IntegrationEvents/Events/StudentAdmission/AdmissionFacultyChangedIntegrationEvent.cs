namespace Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

public sealed class AdmissionFacultyChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public Guid? FirstPriorityFacultyId { get; set; }
    public required string FirstPriorityFacultyName { get; set; }
}