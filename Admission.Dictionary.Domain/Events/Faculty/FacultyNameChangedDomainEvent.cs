using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.Faculty;

public sealed class FacultyNameChangedDomainEvent: IDomainEvent
{
    public FacultyNameChangedDomainEvent()
    {
    }

    internal FacultyNameChangedDomainEvent(Entities.Faculty faculty)
    {
        Id = faculty.Id;
        Name = faculty.Name;
    }
    
    public Guid Id { get; init; }
    public string Name { get; init; }
}