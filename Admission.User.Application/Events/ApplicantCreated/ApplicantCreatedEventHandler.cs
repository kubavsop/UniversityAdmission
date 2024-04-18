using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.Applicant;
using Admission.Domain.Common.Events;
using Admission.User.Domain.Events;

namespace Admission.User.Application.Events.ApplicantCreated;

public sealed class ApplicantCreatedEventHandler: IDomainEventHandler<ApplicantCreatedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public ApplicantCreatedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(ApplicantCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new ApplicantCreatedIntegrationEvent
        {
            Id = notification.Id,
            FullName = notification.FullName,
            Email = notification.Email
        });
        
        return Task.CompletedTask;
    }
}