using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramNameChangedDomainEvent: IDomainEvent
{
    public ProgramNameChangedDomainEvent()
    {
    }

    internal ProgramNameChangedDomainEvent(Entities.EducationProgram program)
    {
        Id = program.Id;
        Name = program.Name;
    }
    
    public Guid Id { get; init; }
    public string Name { get; init; }
}