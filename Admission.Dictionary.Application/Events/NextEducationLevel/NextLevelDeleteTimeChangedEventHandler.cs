using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.NextEducationLevel;
using Admission.Dictionary.Domain.Events.NextEducationLevel;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.NextEducationLevel;

public sealed class NextLevelDeleteTimeChangedEventHandler: IDomainEventHandler<NextLevelDeleteTimeChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public NextLevelDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }


    public Task Handle(NextLevelDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new NextLevelDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}