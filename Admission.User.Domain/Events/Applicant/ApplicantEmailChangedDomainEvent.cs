using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Applicant;

public class ApplicantEmailChangedDomainEvent: IDomainEvent
{
    public ApplicantEmailChangedDomainEvent()
    {
    }

    internal ApplicantEmailChangedDomainEvent(AdmissionUser user)
    {
        Id = user.Id;
        Email = user.Email;
    }
    
    public Guid Id { get; init; }
    
    public string Email { get; init; }
}