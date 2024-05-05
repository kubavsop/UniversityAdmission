using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Applicant;

namespace Admission.User.Application.Events.ApplicantCreated;

public sealed class ApplicantFullNameChangedEventHandler: BaseDomainEventHandler<ApplicantFullNameChangedDomainEvent>
{
    public ApplicantFullNameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ApplicantFullNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ApplicantFullNameChangedIntegrationEvent()
        {
            Id = notification.Id,
            FullName = notification.Fullname
        }, RoutingKeys.ApplicantChangedRoutingKey);

        return Task.CompletedTask;
    }
}