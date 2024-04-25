namespace Admission.DTOs.IntegrationEvents.BaseEvents;

public abstract class DeleteTimeChangedIntegrationEvent: IIntegrationEvent
{
    public Guid Id { get; init; }
    public DateTime? DeleteTime { get; init; }
}