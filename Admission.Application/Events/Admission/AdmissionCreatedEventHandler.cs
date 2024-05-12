using Admission.Application.Constants;
using Admission.Domain.Events.Admission;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

namespace Admission.Application.Events.Admission;

public sealed class AdmissionCreatedEventHandler: BaseDomainEventHandler<AdmissionCreatedDomainEvent>
{
    public AdmissionCreatedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(AdmissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new AdmissionCreatedIntegrationEvent
        {
            Id = notification.Id,
            ApplicantId = notification.ApplicantId,
            ManagerId = notification.ManagerId,
            Status = notification.Status
        }, RoutingKeys.AdmissionChangedRoutingKey);
        
        Publisher.Publish(new MailRequestIntegrationEvent
        {
            EmailTo = notification.Email,
            Subject = "Creating an admission",
            Body = "Congratulations! You create an admission"
        }, RoutingKeys.NotificationRoutingKey);

        return Task.CompletedTask;
    }
}