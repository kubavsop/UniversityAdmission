using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.Passport;

public sealed class PassportChangedDomainEvent: IDomainEvent
{
    public PassportChangedDomainEvent()
    {
    }

    internal PassportChangedDomainEvent(Entities.Passport passport)
    {
        Id = passport.Id;
    }
    
    public Guid Id { get; init; }
}