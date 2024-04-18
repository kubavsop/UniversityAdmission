using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramEducationFormChangeDomainEvent: IDomainEvent
{
    public ProgramEducationFormChangeDomainEvent()
    {
    }

    internal ProgramEducationFormChangeDomainEvent(Entities.EducationProgram program)
    {
        Id = program.Id;
        EducationForm = program.EducationForm;
    }
    
    public Guid Id { get; init; }
    public string EducationForm { get; init; }
}