using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Manager;

public sealed class ManagerCreatedDomainEvent: IDomainEvent
{
    public ManagerCreatedDomainEvent()
    {
    }

    internal ManagerCreatedDomainEvent(AdmissionUser user)
    {
        Id = user.Id;
        Email = user.Email;
        FullName = user.FullName;
    }
    
    public Guid Id { get; init; }
    
    public string Email { get; init; }

    public string FullName { get; init; }
}