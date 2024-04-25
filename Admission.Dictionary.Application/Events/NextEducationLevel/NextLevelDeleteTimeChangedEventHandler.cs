using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.NextEducationLevel;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.NextEducationLevel;

namespace Admission.Dictionary.Application.Events.NextEducationLevel;

public sealed class NextLevelDeleteTimeChangedEventHandler: BaseDomainEventHandler<NextLevelDeleteTimeChangedDomainEvent>
{
    public NextLevelDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    
    public override Task Handle(NextLevelDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new NextLevelDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        }, RoutingKeys.NextLevelChangedRoutingKey);

        return Task.CompletedTask;
    }
}