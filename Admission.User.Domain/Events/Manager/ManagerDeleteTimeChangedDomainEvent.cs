using Admission.Domain.Common.Events;

namespace Admission.User.Domain.Events.Manager;

public sealed class ManagerDeleteTimeChangedDomainEvent: IDomainEvent
{
    public ManagerDeleteTimeChangedDomainEvent()
    {
    }
    
    internal ManagerDeleteTimeChangedDomainEvent(Guid userId, DateTime? deleteTime, string email)
    {
        Id = userId;
        DeleteTime = deleteTime;
        Email = email;
    }
    
    public Guid Id { get; init; }
    
    public DateTime? DeleteTime { get; init; }
    
    public string Email { get; init; }
}