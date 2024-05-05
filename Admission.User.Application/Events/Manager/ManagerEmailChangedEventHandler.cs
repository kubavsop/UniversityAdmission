using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Manager;

namespace Admission.User.Application.Events.Manager;

public sealed class ManagerEmailChangedEventHandler: BaseDomainEventHandler<ManagerEmailChangedDomainEvent>
{
    public ManagerEmailChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ManagerEmailChangedDomainEvent notification, CancellationToken cancellationToken)
    {

        Publisher.Publish(new ManagerEmailChangedIntegrationEvent
        {
            Id = notification.Id,
            Email = notification.Email
        }, RoutingKeys.ManagerChangedRoutingKey);

        return Task.CompletedTask;
    }
}