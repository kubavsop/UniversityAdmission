

using IntegrationEvents;

namespace Admission.IntegrationEvents.Events.EducationProgram;

public sealed class ProgramEducationFormChangeIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required string EducationForm { get; init; }
}