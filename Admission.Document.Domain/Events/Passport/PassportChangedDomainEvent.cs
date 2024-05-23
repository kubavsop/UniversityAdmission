using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.Passport;

public sealed class PassportChangedDomainEvent: IDomainEvent
{
    public PassportChangedDomainEvent()
    {
    }

    internal PassportChangedDomainEvent(Entities.Passport passport)
    {
        UserId = passport.ApplicantId;
    }
    
    public Guid UserId { get; init; }
}