using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramLanguageChangedDomainEvent: IDomainEvent
{
    public ProgramLanguageChangedDomainEvent()
    {
    }

    public ProgramLanguageChangedDomainEvent(Entities.EducationProgram program)
    {
        Id = program.Id;
        Language = program.Language;
    }

    public Guid Id { get; init; }
    public string Language { get; init; }
}