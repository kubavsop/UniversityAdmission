namespace Admission.DTOs.IntegrationEvents.Events.FIle;

public sealed class FileChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid UserId { get; init; }
}