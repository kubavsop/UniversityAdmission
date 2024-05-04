namespace Admission.DTOs.IntegrationEvents.Events.Manager;

public sealed class ManagerCreatedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    
    public required string Email { get; init; }

    public required string FullName { get; init; }
}