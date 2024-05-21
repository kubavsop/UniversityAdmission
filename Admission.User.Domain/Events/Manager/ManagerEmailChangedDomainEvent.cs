using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Manager;

public sealed class ManagerEmailChangedDomainEvent: IDomainEvent
{
    public ManagerEmailChangedDomainEvent()
    {
    }
    internal ManagerEmailChangedDomainEvent(AdmissionUser user)
    {
        Id = user.Id;
        Email = user.Email;
    }
    
    public Guid Id { get; init; }
    
    public string Email { get; init; }
}