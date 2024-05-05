namespace Admission.DTOs.IntegrationEvents.Events.Manager;

public sealed class ManagerFacultyChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required Guid FacultyId { get; init; }
    public required string FacultyName { get; init; }
}