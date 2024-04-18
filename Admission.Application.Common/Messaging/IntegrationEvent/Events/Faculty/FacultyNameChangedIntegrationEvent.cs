using Admission.Application.Common.Messaging.IntegrationEvent.BaseEvents;

namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.Faculty;

public sealed class FacultyNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;