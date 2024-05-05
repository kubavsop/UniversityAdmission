using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Manager;

public sealed class ManagerFullNameChangedDomainEvent: IDomainEvent
{
    public ManagerFullNameChangedDomainEvent()
    {
    }

    internal ManagerFullNameChangedDomainEvent(AdmissionUser user)
    {
        Id = user.Id;
        Fullname = user.FullName;
    }
    
    public Guid Id { get; init; }
    
    public string Fullname { get; init; }
}