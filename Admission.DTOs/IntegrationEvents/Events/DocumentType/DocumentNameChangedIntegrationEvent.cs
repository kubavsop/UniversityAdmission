using Admission.DTOs.IntegrationEvents.BaseEvents;

namespace Admission.DTOs.IntegrationEvents.Events.DocumentType;

public sealed class DocumentNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;