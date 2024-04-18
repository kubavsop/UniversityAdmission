namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;

public sealed class ProgramEducationLevelChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    public required int EducationLevelId { get; init; }
    public required string EducationLevelName { get; init; }
}