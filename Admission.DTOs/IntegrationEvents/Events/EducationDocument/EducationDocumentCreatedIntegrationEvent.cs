namespace Admission.DTOs.IntegrationEvents.Events.EducationDocument;

public sealed class EducationDocumentCreatedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required Guid EducationDocumentTypeId { get; init; }
}