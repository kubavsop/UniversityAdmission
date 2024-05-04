using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Applicant;

namespace Admission.User.Application.Events.ApplicantCreated;

public sealed class ApplicantEmailChangedEventHandler: BaseDomainEventHandler<ApplicantEmailChangedDomainEvent>
{
    public ApplicantEmailChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ApplicantEmailChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ApplicantEmailChangedIntegrationEvent
        {
            Id = notification.Id,
            Email = notification.Email
        }, RoutingKeys.ApplicantChangedRoutingKey);

        return Task.CompletedTask;
    }
}