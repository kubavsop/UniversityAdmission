using Admission.Domain.Common.Entities;

namespace Admission.Domain.Common.Events;

public abstract class DeleteTimeChangedEvent: IDomainEvent
{
    public DeleteTimeChangedEvent()
    {
    }
    public DeleteTimeChangedEvent(BaseEntity entity)
    {
        Id = entity.Id;
        DeleteTime = entity.DeleteTime;
    }
    
    public Guid Id { get; init; }
    public DateTime? DeleteTime { get; init; }
}