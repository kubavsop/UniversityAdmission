using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.File;

public sealed class FileChangedDomainEvent: IDomainEvent
{
    public FileChangedDomainEvent()
    {
    }

    internal FileChangedDomainEvent(Guid userId)
    {
        UserId = userId;
    }
    public Guid UserId { get; init; }
}