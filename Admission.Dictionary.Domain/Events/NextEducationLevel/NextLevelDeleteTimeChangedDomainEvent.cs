using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.NextEducationLevel;

public sealed class NextLevelDeleteTimeChangedDomainEvent: DeleteTimeChangedEvent
{
    public NextLevelDeleteTimeChangedDomainEvent()
    {
    }

    public NextLevelDeleteTimeChangedDomainEvent(Entities.NextEducationLevel nextEducationLevel) : base(nextEducationLevel)
    {
    }
}