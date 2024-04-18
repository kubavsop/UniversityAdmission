using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationLevel;
using Admission.Dictionary.Domain.Events.EducationLevel;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.EducationLevel;

public sealed class LevelDeleteTimeChangedEventHandler: IDomainEventHandler<EducationLevelDeleteTimeChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public LevelDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(EducationLevelDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new LevelDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}