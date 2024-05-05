using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Manager;

namespace Admission.User.Application.Events.Manager;

public sealed class ManagerFullnameChangedEventHandler: BaseDomainEventHandler<ManagerFullNameChangedDomainEvent>
{
    public ManagerFullnameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ManagerFullNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ManagerFullNameChangedIntegrationEvent
        {
            Id = notification.Id,
            FullName = notification.Fullname
        }, RoutingKeys.ManagerChangedRoutingKey);

        return Task.CompletedTask;
    }
}