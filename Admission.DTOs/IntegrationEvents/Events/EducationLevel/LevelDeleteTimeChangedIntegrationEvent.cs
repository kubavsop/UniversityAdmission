using Admission.DTOs.IntegrationEvents.BaseEvents;

namespace Admission.DTOs.IntegrationEvents.Events.EducationLevel;

public sealed class LevelDeleteTimeChangedIntegrationEvent : DeleteTimeChangedIntegrationEvent
{
    public int ExternalId { get; init; }
};