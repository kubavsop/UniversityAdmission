
using Admission.Domain.Common.Events;

namespace Admission.User.Domain.Events.Applicant;

public sealed class ApplicantChangedDomainEvent: IDomainEvent
{
    public ApplicantChangedDomainEvent()
    {
    }

    internal ApplicantChangedDomainEvent(Entities.Applicant applicant)
    {
        Id = applicant.Id;
    }
    
    public Guid Id { get; init; }
}