namespace Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;

public sealed class ProgramFacultyChangedIntegrationEvent: IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid FacultyId { get; init; }
    public required string FacultyName { get; init; }
}