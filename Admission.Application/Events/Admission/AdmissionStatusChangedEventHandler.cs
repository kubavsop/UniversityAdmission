using Admission.Application.Constants;
using Admission.Domain.Events.Admission;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

namespace Admission.Application.Events.Admission;

public sealed class AdmissionStatusChangedEventHandler : BaseDomainEventHandler<AdmissionStatusChangedDomainEvent>
{
    public AdmissionStatusChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(AdmissionStatusChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new AdmissionStatusChangedIntegrationEvent
        {
            Id = notification.Id,
            Status = notification.Status
        }, RoutingKeys.AdmissionChangedRoutingKey);
        
        Publisher.Publish(new MailRequestIntegrationEvent
        {
            EmailTo = notification.Email,
            Subject = "Admission status has been changed",
            Body = $"Your admission status is {notification.Status}"
        }, RoutingKeys.NotificationRoutingKey);

        return Task.CompletedTask;
    }
}