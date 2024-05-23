using Admission.Application.Constants;
using Admission.Domain.Events.Admission;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

namespace Admission.Application.Events.Admission;

public sealed class AdmissionManagerChangedEventHandler: BaseDomainEventHandler<AdmissionManagerChangedDomainEvent>
{
    public AdmissionManagerChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(AdmissionManagerChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new AdmissionManagerChangedIntegrationEvent
        {
            Id = notification.Id,
            ManagerId = notification.ManagerId
        }, RoutingKeys.AdmissionChangedRoutingKey);

        if (notification.ManagerEmail != null)
        {
            Publisher.Publish(new MailRequestIntegrationEvent
            {
                EmailTo = notification.ManagerEmail,
                Subject = "Taking an admission",
                Body = "You take a student admission"
            }, RoutingKeys.NotificationRoutingKey);
        } 
        
        Publisher.Publish(new MailRequestIntegrationEvent
        {
            EmailTo = notification.ApplicantEmail,
            Subject = "Admission status",
            Body = "Your admission has been accepted for consideration."
        }, RoutingKeys.NotificationRoutingKey);        
        return Task.CompletedTask;
    }
}