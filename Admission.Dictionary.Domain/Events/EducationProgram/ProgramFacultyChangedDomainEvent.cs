using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationProgram;

public sealed class ProgramFacultyChangedDomainEvent : IDomainEvent
{
    public ProgramFacultyChangedDomainEvent()
    {
    }

    internal ProgramFacultyChangedDomainEvent(Entities.EducationProgram program, Entities.Faculty faculty)
    {
        Id = program.Id;
        FacultyId = faculty.Id;
        FacultyName = faculty.Name;
    }
    
    public Guid Id { get; init; }
    public Guid FacultyId { get; init; }
    public string FacultyName { get; init; }
}