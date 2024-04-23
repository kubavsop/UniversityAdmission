using Admission.IntegrationEvents.Events.Applicant;
using Admission.User.Domain.Events;
using IntegrationEvents;

namespace Admission.User.Application.Events.ApplicantCreated;

public sealed class ApplicantCreatedEventHandler: BaseDomainEventHandler<ApplicantCreatedDomainEvent>
{
    public ApplicantCreatedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    public override Task Handle(ApplicantCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ApplicantCreatedIntegrationEvent
        {
            Id = notification.Id,
            FullName = notification.FullName,
            Email = notification.Email
        });
        
        return Task.CompletedTask;
    }
}