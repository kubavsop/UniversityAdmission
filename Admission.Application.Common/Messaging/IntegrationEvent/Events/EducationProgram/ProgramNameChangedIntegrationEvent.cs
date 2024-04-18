using Admission.Application.Common.Messaging.IntegrationEvent.BaseEvents;

namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;

public sealed class ProgramNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;