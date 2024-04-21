using IntegrationEvents;

namespace Admission.IntegrationEvents.BaseEvents;

public abstract class NameChangedIntegrationEvent<TId>: IIntegrationEvent
{
    public required TId Id { get; init; }
    public required string Name { get; init; }
}