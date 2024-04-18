using Admission.Application.Common.Messaging.IntegrationEvent.BaseEvents;

namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationLevel;

public sealed class LevelNameChangedIntegrationEvent: NameChangedIntegrationEvent<int>;