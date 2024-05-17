namespace Admission.DTOs.IntegrationEvents.Events.EducationDocument;

public sealed class EducationDocumentNameChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}