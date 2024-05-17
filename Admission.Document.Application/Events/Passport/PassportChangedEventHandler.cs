using Admission.Document.Application.Constants;
using Admission.Document.Domain.Events.Passport;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Passport;

namespace Admission.Document.Application.Events;

public sealed class PassportChangedEventHandler: BaseDomainEventHandler<PassportChangedDomainEvent>
{
    public PassportChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(PassportChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new PassportChangedIntegrationEvent
        {
            Id = notification.Id
        }, RoutingKeys.DataChangedRoutingKey);

        return Task.CompletedTask;
    }
}