using Admission.Domain.Common.Events;
using Admission.Domain.Entities;

namespace Admission.Domain.Events.Admission;

public sealed class AdmissionManagerChangedDomainEvent: IDomainEvent
{
    public AdmissionManagerChangedDomainEvent()
    {
    }

    internal AdmissionManagerChangedDomainEvent(StudentAdmission admission)
    {
        Id = admission.Id;
        ManagerId = admission.Manager?.Id;
        ManagerEmail = admission.Manager?.Email;
    }
    
    public Guid Id { get; init; }
    public Guid? ManagerId { get; init; }
    
    public string? ManagerEmail { get; init; }
}