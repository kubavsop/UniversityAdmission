
namespace Admission.DTOs.IntegrationEvents.Events.EducationProgram;

public sealed class ProgramLanguageChangedIntegrationEvent: IIntegrationEvent
{
    public Guid Id { get; init; }
    public required string Language { get; init; }
}