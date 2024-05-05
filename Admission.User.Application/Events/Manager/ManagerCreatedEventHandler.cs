using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Manager;

namespace Admission.User.Application.Events.Manager;

public sealed class ManagerCreatedEventHandler: BaseDomainEventHandler<ManagerCreatedDomainEvent>
{
    public ManagerCreatedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ManagerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ManagerCreatedIntegrationEvent
        {
            Id = notification.Id,
            FullName = notification.FullName,
            Email = notification.Email
        }, RoutingKeys.NotificationRoutingKey);
        
        Publisher.Publish(new MailRequestIntegrationEvent
        {
            EmailTo = notification.Email,
            Subject = "Creating a manager",
            Body = "Congratulations! You became a manager"
        });

        return Task.CompletedTask;
    }
}