using Admission.Domain.Common.Events;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Manager;

namespace Admission.User.Application.Events.Manager;

public sealed class ManagerDeleteTimeChangedEventHandler: BaseDomainEventHandler<ManagerDeleteTimeChangedDomainEvent>
{
    public ManagerDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ManagerDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ManagerDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        }, RoutingKeys.ManagerExistenceHasChanged);

        if (notification.DeleteTime == null)
        {
            Publisher.Publish(new MailRequestIntegrationEvent
            {
                EmailTo = notification.Email,
                Subject = "Removing a manager",
                Body = "You are no longer a manager"
            }, RoutingKeys.NotificationRoutingKey);
        }
        else
        {
            Publisher.Publish(new MailRequestIntegrationEvent
            {
                EmailTo = notification.Email,
                Subject = "Creating a manager",
                Body = "Congratulations! You became a manager"
            }, RoutingKeys.NotificationRoutingKey);
        }

        return Task.CompletedTask;
    }
}