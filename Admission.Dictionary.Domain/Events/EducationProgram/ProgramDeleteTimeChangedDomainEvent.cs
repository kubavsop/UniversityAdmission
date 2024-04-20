using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramDeleteTimeChangedDomainEvent : DeleteTimeChangedEvent
{
    public ProgramDeleteTimeChangedDomainEvent()
    {
    }

    public ProgramDeleteTimeChangedDomainEvent(Entities.EducationProgram program) : base(program)
    {
    }
}