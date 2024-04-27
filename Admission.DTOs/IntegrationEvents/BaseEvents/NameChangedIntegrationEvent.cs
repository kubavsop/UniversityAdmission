namespace Admission.DTOs.IntegrationEvents.BaseEvents;

public abstract class NameChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}