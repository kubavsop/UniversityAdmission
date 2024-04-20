using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramCodeChangedDomainEvent: IDomainEvent
{
    public ProgramCodeChangedDomainEvent()
    {
    }
    
    public ProgramCodeChangedDomainEvent(Entities.EducationProgram program)
    {
        Id = program.Id;
        Code = program.Code;
    }
    
    public Guid Id { get; init; }
    public string Code { get; init; }
}