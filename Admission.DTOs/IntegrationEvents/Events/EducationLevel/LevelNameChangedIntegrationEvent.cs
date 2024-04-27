using Admission.DTOs.IntegrationEvents.BaseEvents;

namespace Admission.DTOs.IntegrationEvents.Events.EducationLevel;

public sealed class LevelNameChangedIntegrationEvent : NameChangedIntegrationEvent
{
    public int ExternalId { get; init; }
};