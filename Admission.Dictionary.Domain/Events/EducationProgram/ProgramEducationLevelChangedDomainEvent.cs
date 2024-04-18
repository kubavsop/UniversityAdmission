using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramEducationLevelChangedDomainEvent: IDomainEvent
{
    public ProgramEducationLevelChangedDomainEvent()
    {
    }

    internal ProgramEducationLevelChangedDomainEvent(Entities.EducationProgram program, Entities.EducationLevel level)
    {
        Id = program.Id;
        EducationLevelId = level.ExternalId;
        EducationLevelName = level.Name;
    }
    
    public Guid Id { get; init; }
    public int EducationLevelId { get; init; }
    public string EducationLevelName { get; init; }
}