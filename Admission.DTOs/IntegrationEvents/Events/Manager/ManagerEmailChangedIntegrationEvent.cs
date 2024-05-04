namespace Admission.DTOs.IntegrationEvents.Events.Manager;

public sealed class ManagerEmailChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    
    public required string Email { get; init; }
}