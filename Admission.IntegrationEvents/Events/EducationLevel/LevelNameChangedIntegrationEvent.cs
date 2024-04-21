using Admission.IntegrationEvents.BaseEvents;

namespace Admission.IntegrationEvents.Events.EducationLevel;

public sealed class LevelNameChangedIntegrationEvent: NameChangedIntegrationEvent<int>;