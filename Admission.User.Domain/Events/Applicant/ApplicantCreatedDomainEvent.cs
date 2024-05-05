using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Applicant;

public sealed class ApplicantCreatedDomainEvent: IDomainEvent
{
    public ApplicantCreatedDomainEvent()
    {
    }
    
    internal ApplicantCreatedDomainEvent(AdmissionUser user)
    {
        Id = user.Id;
        Email = user.Email;
        FullName = user.FullName;
    }

    public Guid Id { get; init; }
    
    public string Email { get; init; }

    public string FullName { get; init; }
}