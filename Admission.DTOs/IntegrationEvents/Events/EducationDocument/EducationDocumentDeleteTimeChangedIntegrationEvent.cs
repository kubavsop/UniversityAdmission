namespace Admission.DTOs.IntegrationEvents.Events.EducationDocument;

public sealed class EducationDocumentDeleteTimeChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required DateTime? DeleteTime { get; init; }
    public required Guid UserId { get; init; }
}