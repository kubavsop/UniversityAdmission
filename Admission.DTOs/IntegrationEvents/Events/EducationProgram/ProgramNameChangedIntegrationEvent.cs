using Admission.DTOs.IntegrationEvents.BaseEvents;

namespace Admission.DTOs.IntegrationEvents.Events.EducationProgram;

public sealed class ProgramNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;