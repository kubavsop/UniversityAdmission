using Admission.IntegrationEvents.BaseEvents;

namespace Admission.IntegrationEvents.Events.DocumentType;

public sealed class DocumentNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;