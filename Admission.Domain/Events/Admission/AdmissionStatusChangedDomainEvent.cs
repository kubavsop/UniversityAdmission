using Admission.Domain.Common.Enums;
using Admission.Domain.Common.Events;
using Admission.Domain.Entities;

namespace Admission.Domain.Events.Admission;

public sealed class AdmissionStatusChangedDomainEvent: IDomainEvent
{
    public AdmissionStatusChangedDomainEvent()
    {
    }

    internal AdmissionStatusChangedDomainEvent(StudentAdmission admission)
    {
        Id = admission.Id;
        Status = admission.Status;
        Email = admission.Applicant.Email;
    }
    
    public Guid Id { get; init; }
    public AdmissionStatus Status { get; init; }  
    public string Email { get; init; }
}