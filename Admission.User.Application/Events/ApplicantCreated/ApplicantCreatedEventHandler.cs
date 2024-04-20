using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.Applicant;
using Admission.User.Domain.Events;

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