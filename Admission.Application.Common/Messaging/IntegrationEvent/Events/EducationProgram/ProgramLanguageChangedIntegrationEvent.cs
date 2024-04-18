namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;

public sealed class ProgramLanguageChangedIntegrationEvent: IIntegrationEvent
{
    public Guid Id { get; init; }
    public required string Language { get; init; }
}