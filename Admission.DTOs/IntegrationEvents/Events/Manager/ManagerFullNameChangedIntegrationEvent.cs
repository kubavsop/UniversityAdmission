namespace Admission.DTOs.IntegrationEvents.Events.Manager;

public sealed class ManagerFullNameChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }

    public required string FullName { get; init; }
}