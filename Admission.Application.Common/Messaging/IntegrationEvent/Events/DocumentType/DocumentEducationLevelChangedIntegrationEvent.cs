namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;

public sealed class DocumentEducationLevelChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required int EducationLevelId { get; init; }
    public required string EducationLevelName { get; init; }
}