using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Applicant;

namespace Admission.User.Application.Events.ApplicantCreated;

public sealed class ApplicantChangedEventHandler: BaseDomainEventHandler<ApplicantChangedDomainEvent>
{
    public ApplicantChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ApplicantChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ApplicantChangedIntegrationEvent
        {
            Id = notification.Id
        }, RoutingKeys.ApplicantChangedRoutingKey);

        return Task.CompletedTask;
    }
}