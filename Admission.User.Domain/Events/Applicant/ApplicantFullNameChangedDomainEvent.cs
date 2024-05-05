using Admission.Domain.Common.Events;
using Admission.User.Domain.Entities;

namespace Admission.User.Domain.Events.Applicant;

public sealed class ApplicantFullNameChangedDomainEvent: IDomainEvent
{
    public ApplicantFullNameChangedDomainEvent()
    {
    }

    internal ApplicantFullNameChangedDomainEvent(AdmissionUser user)
    {
        Id = user.Id;
        Fullname = user.FullName;
    }
    
    public Guid Id { get; init; }
    
    public string Fullname { get; init; }
}