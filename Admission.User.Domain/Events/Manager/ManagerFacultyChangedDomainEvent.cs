using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Manager;

public sealed class ManagerFacultyChangedDomainEvent: IDomainEvent
{
    public ManagerFacultyChangedDomainEvent()
    {
    }
    internal ManagerFacultyChangedDomainEvent(Entities.Manager manager)
    {
        Id = manager.Id;
        Faculty = manager.Faculty!;
    }
    
    public Guid Id { get; init; }
    
    public Faculty Faculty { get; init; }
}