using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.Faculty;

public sealed class FacultyDeleteTimeChangedDomainEvent: DeleteTimeChangedEvent
{
    public FacultyDeleteTimeChangedDomainEvent()
    {
    }

    public FacultyDeleteTimeChangedDomainEvent(Entities.Faculty faculty) : base(faculty)
    {
    }
}