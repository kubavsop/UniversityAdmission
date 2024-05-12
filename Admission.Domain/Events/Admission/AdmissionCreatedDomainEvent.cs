using Admission.Domain.Common.Enums;
using Admission.Domain.Common.Events;
using Admission.Domain.Entities;

namespace Admission.Domain.Events.Admission;

public sealed class AdmissionCreatedDomainEvent: IDomainEvent
{
    public AdmissionCreatedDomainEvent()
    {
    }
    
    internal AdmissionCreatedDomainEvent(StudentAdmission admission)
    {
        Id = admission.Id;
        ManagerId = admission.ManagerId;
        ApplicantId = admission.ApplicantId;
        Status = admission.Status;
        Email = admission.Applicant.Email;
    }
    
    public Guid Id { get; init; }
    public Guid? ManagerId { get; set; }
    public Guid ApplicantId { get; set; }
    public AdmissionStatus Status { get; set; }
    public string Email { get; set; }
}