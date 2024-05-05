namespace Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

public sealed class AdmissionManagerChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public Guid? ManagerId { get; set; }
}