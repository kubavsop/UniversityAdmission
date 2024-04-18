using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationLevel;
using Admission.Dictionary.Domain.Events.EducationLevel;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.EducationLevel;

public class LevelNameChangedEventHandler: IDomainEventHandler<LevelNameChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public LevelNameChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }


    public Task Handle(LevelNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new LevelNameChangedIntegrationEvent
        {
            Id = notification.Id,
            Name = notification.Name
        });

        return Task.CompletedTask;
    }
}