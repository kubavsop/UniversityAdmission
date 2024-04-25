namespace Admission.DTOs.IntegrationEvents.Events.EducationProgram;

public sealed class ProgramCodeChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required string Code { get; init; }
}