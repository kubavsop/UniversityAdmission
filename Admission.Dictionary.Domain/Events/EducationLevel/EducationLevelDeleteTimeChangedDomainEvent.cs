using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationLevel;

public sealed class EducationLevelDeleteTimeChangedDomainEvent: DeleteTimeChangedEvent
{
    public EducationLevelDeleteTimeChangedDomainEvent()
    {
    }

    public EducationLevelDeleteTimeChangedDomainEvent(Entities.EducationLevel educationLevel) : base(educationLevel)
    {
    }
}