using Admission.DTOs.IntegrationEvents.BaseEvents;

namespace Admission.DTOs.IntegrationEvents.Events.Faculty;

public sealed class FacultyNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;