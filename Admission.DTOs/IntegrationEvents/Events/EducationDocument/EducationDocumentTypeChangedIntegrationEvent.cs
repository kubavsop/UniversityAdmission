namespace Admission.DTOs.IntegrationEvents.Events.EducationDocument;

public sealed class EducationDocumentTypeChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required Guid EducationDocumentTypeId { get; init; }
}